package com.client.manager;

import com.client.base.BaseRestService;
import com.client.base.BaseViewManager;
import com.client.dialog.LocationDialog;
import com.client.service.NotificationService;
import com.common.dto.locations.LocationCreate;
import com.common.dto.locations.LocationRead;
import com.common.dto.locations.LocationUpdate;
import com.vaadin.flow.component.dialog.Dialog;
import com.vaadin.flow.component.grid.Grid;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.value.ValueChangeMode;
import org.springframework.stereotype.Service;

import java.util.function.Consumer;

@Service
public class LocationManager extends BaseViewManager<LocationCreate, LocationRead, LocationUpdate> {

    public LocationManager(BaseRestService<LocationCreate, LocationRead, LocationUpdate> baseRestService,
                           NotificationService notificationService, LocationDialog dialog) {
        super(baseRestService, notificationService, LocationRead.class, dialog);
    }

    @Override
    protected void configureColumns(Grid<LocationRead> grid) {
        grid.addColumn(LocationRead::id)
                .setHeader("Ид.")
                .setWidth("80px")
                .setFlexGrow(0)
                .setSortable(true);
        grid.addColumn(LocationRead::address)
                .setHeader("Адрес")
                .setTooltipGenerator(LocationRead::address)
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(LocationRead::rackNumber)
                .setHeader("Стойка")
                .setTooltipGenerator(LocationRead::rackNumber)
                .setAutoWidth(true)
                .setResizable(true)
                .setSortable(true);
        grid.addColumn(d -> d.inventories().stream()
                        .map(LocationRead.InventoryRefInLocationRead::serialNumber).toList())
                .setHeader("Расположены")
                .setTooltipGenerator(d -> d.inventories().stream()
                        .map(LocationRead.InventoryRefInLocationRead::serialNumber).toList().toString())
                .setResizable(true)
                .setSortable(true);
    }

    @Override
    protected Dialog createDialog(Consumer<LocationCreate> handler) {
        return getDialog().createDialog("Новая локация", LocationCreate.class, handler);
    }

    @Override
    protected Dialog updateDialog(LocationRead item, Consumer<LocationUpdate> handler) {
        return getDialog().updateDialog("Редактирование", LocationUpdate.class, item, handler);
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
                        i.rackNumber() != null && i.rackNumber().toLowerCase().contains(filter.toLowerCase())
                );
            }
        });

        return searchField;
    }

}
