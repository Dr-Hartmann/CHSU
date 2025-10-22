package com.client.dialog;

import com.client.base.BaseViewDialog;
import com.client.service.EquipmentTypeService;
import com.client.service.ManufacturerService;
import com.client.service.NotificationService;
import com.common.dto.equipment_types.EquipmentTypeRead;
import com.common.dto.equipments.EquipmentCreate;
import com.common.dto.equipments.EquipmentRead;
import com.common.dto.equipments.EquipmentUpdate;
import com.common.dto.manufacturers.ManufacturerRead;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.combobox.ComboBox;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.binder.BeanValidationBinder;
import org.springframework.stereotype.Service;

import java.util.Objects;

@Service
public class EquipmentDialog extends BaseViewDialog<EquipmentCreate, EquipmentRead, EquipmentUpdate> {

    final TextField name = new TextField("Название");
    final ComboBox<EquipmentTypeRead> type = new ComboBox<>("Тип оборудования");
    final ComboBox<ManufacturerRead> manufacturer = new ComboBox<>("Производитель");

    final EquipmentTypeService equipmentTypeService;
    final ManufacturerService manufacturerService;

    java.util.List<EquipmentTypeRead> types;
    java.util.List<ManufacturerRead> manufacturers;

    public EquipmentDialog(NotificationService notificationService, EquipmentTypeService equipmentTypeService,
                           ManufacturerService manufacturerService) {

        super(notificationService);

        this.equipmentTypeService = equipmentTypeService;
        this.manufacturerService = manufacturerService;

        type.setItemLabelGenerator(EquipmentTypeRead::name);
        manufacturer.setItemLabelGenerator(ManufacturerRead::name);
    }

    @Override
    protected Component[] fields() {
        return new Component[]{name, type, manufacturer};
    }

    @Override
    protected void bindCreate(BeanValidationBinder<EquipmentCreate> binder) {
        types = equipmentTypeService.readAll();
        manufacturers = manufacturerService.readAll();
        type.setItems(types);
        manufacturer.setItems(manufacturers);

        binder.forField(name).bind("name");

        binder.forField(type)
                .withConverter(
                        r -> r == null ? null : new EquipmentCreate.EquipmentTypeInRefInEquipmentCreate(r.id()),
                        _ -> null
                )
                .bind("type");

        binder.forField(manufacturer)
                .withConverter(
                        r -> r == null ? null : new EquipmentCreate.ManufacturerRefInEquipmentCreate(r.id()),
                        _ -> null
                )
                .bind("manufacturer");
    }

    @Override
    protected void bindUpdate(BeanValidationBinder<EquipmentUpdate> binder) {
        types = equipmentTypeService.readAll();
        manufacturers = manufacturerService.readAll();
        type.setItems(types);
        manufacturer.setItems(manufacturers);

        binder.forField(name).bind("name");

        binder.forField(type)
                .withConverter(
                        r -> r == null ? null : new EquipmentUpdate.EquipmentTypeRefInEquipmentUpdate(r.id()),
                        _ -> null
                )
                .bind("type");

        binder.forField(manufacturer)
                .withConverter(
                        r -> r == null ? null : new EquipmentUpdate.ManufacturerRefInEquipmentUpdate(r.id()),
                        _ -> null
                )
                .bind("manufacturer");
    }

    @Override
    protected void fillFields(EquipmentRead item) {
        if (item == null) return;

        name.setValue(item.name());

        type.setValue(types.stream()
                .filter(t -> Objects.equals(t.name(), item.type().name()))
                .findFirst()
                .orElse(type.getEmptyValue()));

        manufacturer.setValue(manufacturers.stream()
                .filter(m -> Objects.equals(m.name(), item.manufacturer().name()))
                .findFirst()
                .orElse(manufacturer.getEmptyValue()));
    }

    @Override
    protected EquipmentCreate createDto() {
        return new EquipmentCreate(
                name.getValue(),
                new EquipmentCreate.EquipmentTypeInRefInEquipmentCreate(type.getValue().id()),
                new EquipmentCreate.ManufacturerRefInEquipmentCreate(manufacturer.getValue().id()));
    }

    @Override
    protected EquipmentUpdate updateDto(EquipmentRead item) {
        return new EquipmentUpdate(item.id(), name.getValue(),
                new EquipmentUpdate.EquipmentTypeRefInEquipmentUpdate(type.getValue().id()),
                new EquipmentUpdate.ManufacturerRefInEquipmentUpdate(manufacturer.getValue().id()));
    }

}
