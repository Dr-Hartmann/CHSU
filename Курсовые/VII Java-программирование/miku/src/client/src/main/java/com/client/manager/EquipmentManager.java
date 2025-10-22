package com.client.manager;

import com.client.base.BaseRestService;
import com.client.base.BaseViewManager;
import com.client.dialog.EquipmentDialog;
import com.client.service.NotificationService;
import com.common.dto.equipments.EquipmentCreate;
import com.common.dto.equipments.EquipmentRead;
import com.common.dto.equipments.EquipmentUpdate;
import com.vaadin.flow.component.dialog.Dialog;
import com.vaadin.flow.component.grid.Grid;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.value.ValueChangeMode;
import org.springframework.stereotype.Service;

import java.util.function.Consumer;

@Service
public class EquipmentManager extends BaseViewManager<EquipmentCreate, EquipmentRead, EquipmentUpdate> {

    public EquipmentManager(BaseRestService<EquipmentCreate, EquipmentRead, EquipmentUpdate> baseRestService,
                            NotificationService notificationService, EquipmentDialog dialog) {
        super(baseRestService, notificationService, EquipmentRead.class, dialog);
    }

    @Override
    protected void configureColumns(Grid<EquipmentRead> grid) {
        grid.addColumn(EquipmentRead::id)
                .setHeader("Ид.")
                .setWidth("80px")
                .setFlexGrow(0)
                .setSortable(true);
        grid.addColumn(EquipmentRead::name)
                .setHeader("Имя")
                .setTooltipGenerator(EquipmentRead::name)
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.type().name())
                .setHeader("Тип")
                .setTooltipGenerator(d -> d.type().name())
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.manufacturer().name())
                .setHeader("Производитель")
                .setTooltipGenerator(d -> d.manufacturer().name())
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.manufacturer().country())
                .setHeader("Страна")
                .setTooltipGenerator(d -> d.manufacturer().country())
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.inventories().stream()
                        .map(EquipmentRead.InventoryRefInEquipmentRead::serialNumber).toList())
                .setHeader("Использован")
                .setTooltipGenerator(d -> d.inventories().stream()
                        .map(EquipmentRead.InventoryRefInEquipmentRead::serialNumber).toList().toString())
                .setResizable(true)
                .setSortable(true);
    }

    @Override
    protected Dialog createDialog(Consumer<EquipmentCreate> handler) {
        return getDialog().createDialog("Новое оборудование", EquipmentCreate.class, handler);
    }

    @Override
    protected Dialog updateDialog(EquipmentRead item, Consumer<EquipmentUpdate> handler) {
        return getDialog().updateDialog("Редактирование", EquipmentUpdate.class, item, handler);
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
