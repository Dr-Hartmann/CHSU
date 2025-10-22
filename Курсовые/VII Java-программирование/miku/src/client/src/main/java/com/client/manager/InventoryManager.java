package com.client.manager;

import com.client.base.BaseRestService;
import com.client.base.BaseViewManager;
import com.client.dialog.InventoryDialog;
import com.client.service.NotificationService;
import com.common.dto.inventories.InventoryCreate;
import com.common.dto.inventories.InventoryRead;
import com.common.dto.inventories.InventoryUpdate;
import com.vaadin.flow.component.dialog.Dialog;
import com.vaadin.flow.component.grid.Grid;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.value.ValueChangeMode;
import org.springframework.stereotype.Service;

import java.time.ZoneId;
import java.time.format.DateTimeFormatter;
import java.util.function.Consumer;

@Service
public class InventoryManager extends BaseViewManager<InventoryCreate, InventoryRead, InventoryUpdate> {

    public InventoryManager(BaseRestService<InventoryCreate, InventoryRead, InventoryUpdate> baseRestService,
                            NotificationService notificationService, InventoryDialog dialog) {
        super(baseRestService, notificationService, InventoryRead.class, dialog);
    }

    @Override
    protected void configureColumns(Grid<InventoryRead> grid) {
        grid.addColumn(InventoryRead::id)
                .setHeader("Ид.")
                .setWidth("80px")
                .setFlexGrow(0)
                .setSortable(true);
        grid.addColumn(InventoryRead::serialNumber)
                .setHeader("Серийный номер")
                .setTooltipGenerator(InventoryRead::serialNumber)
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(InventoryRead::statusDescription)
                .setHeader("Статус")
                .setTooltipGenerator(InventoryRead::statusDescription)
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.equipment().name())
                .setHeader("Оборудование")
                .setTooltipGenerator(d -> d.equipment().name())
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.location().address())
                .setHeader("Адрес")
                .setTooltipGenerator(d -> d.location().address())
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.location().rackNumber())
                .setHeader("Стойка")
                .setTooltipGenerator(d -> d.location().rackNumber())
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        var pattern = "dd.MM.yyyy HH:mm";
        grid.addColumn(d -> DateTimeFormatter.ofPattern(pattern)
                        .withZone(ZoneId.systemDefault())
                        .format(d.createdDate()))
                .setHeader("Создан")
                .setTooltipGenerator(d -> DateTimeFormatter.ofPattern(pattern)
                        .withZone(ZoneId.systemDefault())
                        .format(d.createdDate()))
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> DateTimeFormatter.ofPattern(pattern)
                        .withZone(ZoneId.systemDefault())
                        .format(d.lastModifiedDate()))
                .setHeader("Обновлён")
                .setTooltipGenerator(d -> DateTimeFormatter.ofPattern(pattern)
                        .withZone(ZoneId.systemDefault())
                        .format(d.lastModifiedDate()))
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
    }

    @Override
    protected Dialog createDialog(Consumer<InventoryCreate> handler) {
        return getDialog().createDialog("Новый инвентарь", InventoryCreate.class, handler);
    }

    @Override
    protected Dialog updateDialog(InventoryRead item, Consumer<InventoryUpdate> handler) {
        return getDialog().updateDialog("Редактирование", InventoryUpdate.class, item, handler);
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
                        i.serialNumber() != null && i.serialNumber().toLowerCase().contains(filter.toLowerCase())
                );
            }
        });

        return searchField;
    }

}
