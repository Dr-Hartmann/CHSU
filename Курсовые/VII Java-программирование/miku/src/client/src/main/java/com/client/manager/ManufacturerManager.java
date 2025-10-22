package com.client.manager;

import com.client.base.BaseRestService;
import com.client.base.BaseViewManager;
import com.client.dialog.ManufacturerDialog;
import com.client.service.NotificationService;
import com.common.dto.manufacturers.ManufacturerCreate;
import com.common.dto.manufacturers.ManufacturerRead;
import com.common.dto.manufacturers.ManufacturerUpdate;
import com.vaadin.flow.component.dialog.Dialog;
import com.vaadin.flow.component.grid.Grid;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.value.ValueChangeMode;
import org.springframework.stereotype.Service;

import java.util.function.Consumer;

@Service
public class ManufacturerManager extends BaseViewManager<ManufacturerCreate, ManufacturerRead, ManufacturerUpdate> {

    public ManufacturerManager(BaseRestService<ManufacturerCreate, ManufacturerRead, ManufacturerUpdate> baseRestService,
                               NotificationService notificationService, ManufacturerDialog dialog) {
        super(baseRestService, notificationService, ManufacturerRead.class, dialog);
    }

    @Override
    protected void configureColumns(Grid<ManufacturerRead> grid) {
        grid.addColumn(ManufacturerRead::id)
                .setHeader("Ид.")
                .setWidth("80px")
                .setFlexGrow(0)
                .setSortable(true);
        grid.addColumn(ManufacturerRead::name)
                .setHeader("Имя")
                .setTooltipGenerator(ManufacturerRead::name)
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(ManufacturerRead::country)
                .setHeader("Страна")
                .setTooltipGenerator(ManufacturerRead::country)
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.equipments().stream()
                        .map(ManufacturerRead.EquipmentRefInManufacturerRead::name).toList())
                .setHeader("Оборудование")
                .setTooltipGenerator(d -> d.equipments().stream()
                        .map(ManufacturerRead.EquipmentRefInManufacturerRead::name).toList().toString())
                .setResizable(true)
                .setSortable(true);
    }

    @Override
    protected Dialog createDialog(Consumer<ManufacturerCreate> handler) {
        return getDialog().createDialog("Новый производитель", ManufacturerCreate.class, handler);
    }

    @Override
    protected Dialog updateDialog(ManufacturerRead item, Consumer<ManufacturerUpdate> handler) {
        return getDialog().updateDialog("Редактирование", ManufacturerUpdate.class, item, handler);
    }

    @Override
    public TextField initFilterField() {
        var searchField = new TextField("Поиск по имени");
        searchField.setValueChangeMode(ValueChangeMode.LAZY);

        searchField.addValueChangeListener(event -> {
            var filter = event.getValue();

            var dataProvider = getRestService().getDataProvider();
            if (filter == null || filter.isBlank()) {
                dataProvider.clearFilters();
            } else {
                dataProvider.setFilter(i ->
                        i.name() != null && i.name().toLowerCase().contains(filter.toLowerCase())
                );
            }
        });

        return searchField;
    }

}
