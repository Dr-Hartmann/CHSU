package com.client.manager;

import com.client.base.BaseRestService;
import com.client.base.BaseViewManager;
import com.client.dialog.EquipmentTypeDialog;
import com.client.service.NotificationService;
import com.common.dto.equipment_types.EquipmentTypeCreate;
import com.common.dto.equipment_types.EquipmentTypeRead;
import com.common.dto.equipment_types.EquipmentTypeUpdate;
import com.vaadin.flow.component.dialog.Dialog;
import com.vaadin.flow.component.grid.Grid;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.value.ValueChangeMode;
import org.springframework.stereotype.Service;

import java.util.function.Consumer;

@Service
public class EquipmentTypeManager extends BaseViewManager<EquipmentTypeCreate, EquipmentTypeRead, EquipmentTypeUpdate> {

    public EquipmentTypeManager(BaseRestService<EquipmentTypeCreate, EquipmentTypeRead, EquipmentTypeUpdate> baseRestService,
                                NotificationService notificationService, EquipmentTypeDialog dialog) {
        super(baseRestService, notificationService, EquipmentTypeRead.class, dialog);
    }

    @Override
    protected void configureColumns(Grid<EquipmentTypeRead> grid) {
        grid.addColumn(EquipmentTypeRead::id)
                .setHeader("Ид.")
                .setWidth("80px")
                .setFlexGrow(0)
                .setSortable(true);
        grid.addColumn(EquipmentTypeRead::name)
                .setHeader("Имя")
                .setTooltipGenerator(EquipmentTypeRead::name)
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.equipments().stream()
                        .map(EquipmentTypeRead.EquipmentRefInTypeRead::name).toList())
                .setHeader("Оборудование")
                .setTooltipGenerator(d -> d.equipments().stream()
                        .map(EquipmentTypeRead.EquipmentRefInTypeRead::name).toList().toString())
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
    }

    @Override
    protected Dialog createDialog(Consumer<EquipmentTypeCreate> handler) {
        return getDialog().createDialog("Новый тип оборудования", EquipmentTypeCreate.class, handler);
    }

    @Override
    protected Dialog updateDialog(EquipmentTypeRead item, Consumer<EquipmentTypeUpdate> handler) {
        return getDialog().updateDialog("Редактирование", EquipmentTypeUpdate.class, item, handler);
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
