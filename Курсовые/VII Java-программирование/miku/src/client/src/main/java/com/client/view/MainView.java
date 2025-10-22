package com.client.view;

import com.client.ClientConfig;
import com.client.dashboard.*;
import com.client.service.*;
import com.client.view.ui.ViewToolbar;
import com.vaadin.flow.component.dashboard.Dashboard;
import com.vaadin.flow.component.dashboard.DashboardWidget;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.dom.Style;
import com.vaadin.flow.router.Route;

@Route
public class MainView extends VerticalLayout {

    public MainView(EquipmentService equipmentService, EquipmentTypeService equipmentTypeService,
                    ManufacturerService manufacturerService, LocationService locationService,
                    InventoryService inventoryService, NotificationService notificationService, ClientConfig clientConfig) {

        setSizeFull();
        setPadding(false);
        setSpacing(false);
        getStyle().setOverflow(Style.Overflow.HIDDEN);
        add(new ViewToolbar("Дэшборд"));

        var dashboard = new Dashboard();
        dashboard.setSizeFull();

        var qrWidget = new DashboardWidget("Войти в мобильную версию", new QrCodeCurrentLink(notificationService, clientConfig));
        qrWidget.setColspan(1);
        dashboard.addWidgetAtIndex(0, qrWidget);

        dashboard.addWidgetAtIndex(1, new DashboardWidget(new LoadGauge()));

        var chartWidget = new DashboardWidget("Статистика оборудования", new ColumnChart(equipmentService));
        chartWidget.setColspan(4);
        dashboard.addWidgetAtIndex(2, chartWidget);

        dashboard.addWidgetAtIndex(3, new DashboardWidget("Статистика производителей", new PieChart(inventoryService)));

        var kpiCard = new KpiCard(equipmentService, equipmentTypeService, manufacturerService, locationService, inventoryService);
        var kpi = new DashboardWidget("Статистика использования аппаратной инфраструктуры", kpiCard.getKpiCards());
        kpi.setColspan(1);
        dashboard.addWidgetAtIndex(4, kpi);

        dashboard.setEditable(true);
        add(dashboard);
    }

}
