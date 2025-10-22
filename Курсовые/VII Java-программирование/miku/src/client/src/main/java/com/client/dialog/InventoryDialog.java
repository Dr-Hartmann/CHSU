package com.client.dialog;

import com.client.base.BaseViewDialog;
import com.client.service.EquipmentService;
import com.client.service.LocationService;
import com.client.service.NotificationService;
import com.common.dto.equipments.EquipmentRead;
import com.common.dto.inventories.InventoryCreate;
import com.common.dto.inventories.InventoryRead;
import com.common.dto.inventories.InventoryStatus;
import com.common.dto.inventories.InventoryUpdate;
import com.common.dto.locations.LocationRead;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.combobox.ComboBox;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.binder.BeanValidationBinder;
import org.springframework.stereotype.Service;

import java.util.Arrays;
import java.util.List;
import java.util.Objects;

@Service
public class InventoryDialog extends BaseViewDialog<InventoryCreate, InventoryRead, InventoryUpdate> {

    final TextField serialNumber = new TextField("Серийный номер");
    final ComboBox<EquipmentRead> equipment = new ComboBox<>("Оборудование");
    final ComboBox<LocationRead> location = new ComboBox<>("Расположение");
    final ComboBox<InventoryStatus> status = new ComboBox<>("Статус");

    final EquipmentService equipmentService;
    final LocationService locationService;

    List<EquipmentRead> equipments;
    List<LocationRead> locations;
    InventoryStatus[] statuses;

    public InventoryDialog(NotificationService notificationService,
                           EquipmentService equipmentService, LocationService locationService) {
        super(notificationService);

        this.equipmentService = equipmentService;
        this.locationService = locationService;

        statuses = InventoryStatus.values();
        equipment.setItemLabelGenerator(EquipmentRead::name);
        location.setItemLabelGenerator(l -> l.address() + " - " + l.rackNumber());
        status.setItemLabelGenerator(InventoryStatus::getDescription);
        status.setItems(statuses);
    }

    @Override
    protected Component[] fields() {
        return new Component[]{serialNumber, equipment, location, status};
    }

    @Override
    protected void bindCreate(BeanValidationBinder<InventoryCreate> binder) {
        equipments = equipmentService.readAll();
        locations = locationService.readAll();
        equipment.setItems(equipments);
        location.setItems(locations);

        binder.forField(serialNumber).bind("serialNumber");

        binder.forField(equipment)
                .withConverter(
                        r -> r == null ? null : new InventoryCreate.EquipmentRefInInventoryCreate(r.id()),
                        _ -> null
                )
                .bind("equipment");

        binder.forField(location)
                .withConverter(
                        r -> r == null ? null : new InventoryCreate.LocationRefInInventoryCreate(r.id()),
                        _ -> null
                )
                .bind("location");

        binder.forField(status).bind("status");
    }

    @Override
    protected void bindUpdate(BeanValidationBinder<InventoryUpdate> binder) {
        equipments = equipmentService.readAll();
        locations = locationService.readAll();
        equipment.setItems(equipments);
        location.setItems(locations);

        binder.forField(serialNumber).bind("serialNumber");

        binder.forField(equipment)
                .withConverter(
                        r -> r == null ? null : new InventoryUpdate.EquipmentRefInInventoryUpdate(r.id()),
                        _ -> null
                )
                .bind("equipment");

        binder.forField(location)
                .withConverter(
                        r -> r == null ? null : new InventoryUpdate.LocationRefInInventoryUpdate(r.id()),
                        _ -> null
                )
                .bind("location");

        binder.forField(status).bind("status");
    }

    @Override
    protected void fillFields(InventoryRead item) {
        if (item == null) return;

        serialNumber.setValue(item.serialNumber());

        equipment.setValue(equipments.stream()
                .filter(m -> Objects.equals(m.name(), item.equipment().name()))
                .findFirst()
                .orElse(equipment.getEmptyValue()));

        location.setValue(locations.stream()
                .filter(l -> Objects.equals(l.address() + " - " + l.rackNumber(), item.location().address() + " - " + item.location().rackNumber()))
                .findFirst()
                .orElse(location.getEmptyValue()));

        status.setValue(Arrays.stream(statuses).toList().stream()
                .filter(m -> Objects.equals(m.name(), item.status().name()))
                .findFirst()
                .orElse(status.getEmptyValue()));
    }

    @Override
    protected InventoryCreate createDto() {
        return new InventoryCreate(
                serialNumber.getValue(),
                new InventoryCreate.EquipmentRefInInventoryCreate(equipment.getValue().id()),
                new InventoryCreate.LocationRefInInventoryCreate(location.getValue().id()),
                status.getValue());
    }

    @Override
    protected InventoryUpdate updateDto(InventoryRead item) {
        return new InventoryUpdate(item.id(),
                serialNumber.getValue(),
                new InventoryUpdate.EquipmentRefInInventoryUpdate(equipment.getValue().id()),
                new InventoryUpdate.LocationRefInInventoryUpdate(location.getValue().id()),
                status.getValue());
    }

}
