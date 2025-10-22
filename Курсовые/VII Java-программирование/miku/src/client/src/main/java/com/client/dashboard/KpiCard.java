package com.client.dashboard;

import com.client.base.BaseRestService;
import com.client.service.*;
import com.vaadin.flow.component.AttachEvent;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.DetachEvent;
import com.vaadin.flow.component.html.H2;
import com.vaadin.flow.component.html.Span;
import com.vaadin.flow.component.icon.VaadinIcon;
import com.vaadin.flow.component.orderedlayout.FlexLayout;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.shared.Registration;
import com.vaadin.flow.spring.annotation.UIScope;
import com.vaadin.flow.theme.lumo.LumoUtility;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;

import java.text.NumberFormat;
import java.util.Locale;
import java.util.function.Function;

@RequiredArgsConstructor
public class KpiCard extends VerticalLayout {

    private final transient EquipmentService equipmentService;
    private final transient EquipmentTypeService equipmentTypeService;
    private final transient ManufacturerService manufacturerService;
    private final transient LocationService locationService;
    private final transient InventoryService inventoryService;

    private String channelId;
    private final Span span = new Span("0");
    private transient Function<?, String> prefix;
    private Registration registration;

    private static final NumberFormat FORMATTER = NumberFormat.getNumberInstance(Locale.FRANCE);

    public Component getKpiCards() {
        var dashboardGrid = new FlexLayout();
        dashboardGrid.setFlexWrap(FlexLayout.FlexWrap.WRAP);
        dashboardGrid.setJustifyContentMode(JustifyContentMode.CENTER);
        dashboardGrid.addClassNames(LumoUtility.Gap.MEDIUM);
        dashboardGrid.getChildren().forEach(card -> {
            card.getElement().getStyle().set("flex-grow", "1");
            card.getElement().getStyle().set("flex-basis", "300px");
        });

        dashboardGrid.add(
                of("Доступного сетевое оборудование", _ -> String.valueOf(equipmentService.readAll()
                                .stream()
                                .filter(i -> !i.inventories().isEmpty())
                                .count()),
                        "узлов", VaadinIcon.CONNECT, equipmentService),
                of("Производителей", _ -> String.valueOf(manufacturerService.readAll()
                                .stream()
                                .filter(i -> !i.equipments().isEmpty())
                                .count()),
                        "компаний", VaadinIcon.FACTORY, manufacturerService),
                of("Типов сетевого оборудования", _ -> String.valueOf(equipmentTypeService.readAll()
                                .stream()
                                .filter(i -> !i.equipments().isEmpty())
                                .count()),
                        "типов", VaadinIcon.OPTIONS, equipmentTypeService),
                of("Локаций размещения", _ -> String.valueOf(locationService.readAll()
                                .stream()
                                .filter(i -> !i.inventories().isEmpty())
                                .count()),
                        "мест", VaadinIcon.MAP_MARKER, locationService),
                of("Инвентарь", _ -> "", "ед.", VaadinIcon.PACKAGE, inventoryService)
        );

        return dashboardGrid;
    }

    private KpiCard of(String title, Function<?, String> prefix, String unit, VaadinIcon icon, BaseRestService<?, ?, ?> restService) {

        var kpiCard = new KpiCard(equipmentService, equipmentTypeService, manufacturerService, locationService, inventoryService);

        kpiCard.channelId = restService.getClass().getSimpleName();
        kpiCard.prefix = prefix;

        kpiCard.setSpacing(false);
        kpiCard.addClassNames(
                LumoUtility.Background.BASE,
                LumoUtility.BorderRadius.MEDIUM,
                LumoUtility.BoxShadow.SMALL,
                LumoUtility.Padding.MEDIUM,
                LumoUtility.Display.FLEX,
                LumoUtility.Width.MEDIUM
        );

        var iconElement = icon.create();
        iconElement.addClassNames(
                LumoUtility.TextColor.PRIMARY,
                LumoUtility.IconSize.SMALL
        );

        var label = new Span(title);
        label.addClassNames(
                LumoUtility.TextColor.SECONDARY,
                LumoUtility.FontSize.SMALL,
                LumoUtility.FontWeight.MEDIUM
        );

        var header = new HorizontalLayout(iconElement, label);
        header.addClassNames(LumoUtility.AlignItems.CENTER, LumoUtility.Gap.SMALL);

        var unitSpan = new Span(unit);
        unitSpan.addClassNames(
                LumoUtility.FontSize.MEDIUM,
                LumoUtility.TextColor.SECONDARY,
                LumoUtility.Margin.Left.SMALL
        );

        var valueContainer = new H2(kpiCard.span, unitSpan);
        valueContainer.addClassNames(LumoUtility.Margin.NONE, LumoUtility.FontSize.XXLARGE);

        kpiCard.add(header, valueContainer);
        return kpiCard;
    }

    private void updateValue(Object count) {
        if (count instanceof Number num) {
            var p = prefix.apply(null);
            span.setText((p.isBlank() ? "" : p + " / ") + FORMATTER.format(num));
        } else {
            span.setText(String.valueOf(count));
        }
    }

    @Override
    protected void onAttach(AttachEvent attachEvent) {
        super.onAttach(attachEvent);
        var ui = attachEvent.getUI();
        registration = DashboardBroadcaster.register(channelId, count -> {
            if (ui.isAttached()) {
                ui.access(() -> updateValue(count));
            }
        });
    }

    @Override
    protected void onDetach(DetachEvent detachEvent) {
        if (registration != null) {
            registration.remove();
            registration = null;
        }
        super.onDetach(detachEvent);
    }

}
