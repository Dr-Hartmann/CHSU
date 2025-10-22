package com.client.dialog;

import com.client.base.BaseViewDialog;
import com.client.service.NotificationService;
import com.common.dto.manufacturers.ManufacturerCreate;
import com.common.dto.manufacturers.ManufacturerRead;
import com.common.dto.manufacturers.ManufacturerUpdate;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.binder.BeanValidationBinder;
import org.springframework.stereotype.Service;

@Service
public class ManufacturerDialog extends BaseViewDialog<ManufacturerCreate, ManufacturerRead, ManufacturerUpdate> {

    final TextField name = new TextField("Название");
    final TextField country = new TextField("Страна");

    public ManufacturerDialog(NotificationService notificationService) {
        super(notificationService);
    }

    @Override
    protected Component[] fields() {
        return new Component[]{name, country};
    }

    @Override
    protected void bindCreate(BeanValidationBinder<ManufacturerCreate> binder) {
        binder.forField(name).bind("name");
        binder.forField(country).bind("country");
    }

    @Override
    protected void bindUpdate(BeanValidationBinder<ManufacturerUpdate> binder) {
        binder.forField(name).bind("name");
        binder.forField(country).bind("country");
    }

    @Override
    protected void fillFields(ManufacturerRead item) {
        if (item == null) return;
        name.setValue(item.name());
        country.setValue(item.country());
    }

    @Override
    protected ManufacturerCreate createDto() {
        return new ManufacturerCreate(name.getValue(), country.getValue());
    }

    @Override
    protected ManufacturerUpdate updateDto(ManufacturerRead item) {
        return new ManufacturerUpdate(item.id(), name.getValue(), country.getValue());
    }

}
