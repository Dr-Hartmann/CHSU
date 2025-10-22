package com.client.dialog;

import com.client.base.BaseViewDialog;
import com.client.service.NotificationService;
import com.common.dto.equipment_types.EquipmentTypeCreate;
import com.common.dto.equipment_types.EquipmentTypeRead;
import com.common.dto.equipment_types.EquipmentTypeUpdate;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.binder.BeanValidationBinder;
import org.springframework.stereotype.Service;

@Service
public class EquipmentTypeDialog extends BaseViewDialog<EquipmentTypeCreate, EquipmentTypeRead, EquipmentTypeUpdate> {

    final TextField nameField = new TextField("Название");

    public EquipmentTypeDialog(NotificationService notificationService) {
        super(notificationService);
    }

    @Override
    protected Component[] fields() {
        return new Component[]{nameField};
    }

    @Override
    protected void bindCreate(BeanValidationBinder<EquipmentTypeCreate> binder) {
        binder.forField(nameField).bind("name");
    }

    @Override
    protected void bindUpdate(BeanValidationBinder<EquipmentTypeUpdate> binder) {
        binder.forField(nameField).bind("name");
    }

    @Override
    protected void fillFields(EquipmentTypeRead item) {
        if (item == null) return;
        nameField.setValue(item.name());
    }

    @Override
    protected EquipmentTypeCreate createDto() {
        return new EquipmentTypeCreate(nameField.getValue());
    }

    @Override
    protected EquipmentTypeUpdate updateDto(EquipmentTypeRead item) {
        return new EquipmentTypeUpdate(item.id(), nameField.getValue());
    }

}
