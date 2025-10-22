package com.client.dialog;

import com.client.base.BaseViewDialog;
import com.client.service.NotificationService;
import com.common.dto.locations.LocationCreate;
import com.common.dto.locations.LocationRead;
import com.common.dto.locations.LocationUpdate;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.data.binder.BeanValidationBinder;
import org.springframework.stereotype.Service;

@Service
public class LocationDialog extends BaseViewDialog<LocationCreate, LocationRead, LocationUpdate> {

    final TextField address = new TextField("Адрес");
    final TextField rack = new TextField("Стойка");

    public LocationDialog(NotificationService notificationService) {
        super(notificationService);
    }

    @Override
    protected Component[] fields() {
        return new Component[]{address, rack};
    }

    @Override
    protected void bindCreate(BeanValidationBinder<LocationCreate> binder) {
        binder.forField(address).bind("address");
        binder.forField(rack).bind("rackNumber");
    }

    @Override
    protected void bindUpdate(BeanValidationBinder<LocationUpdate> binder) {
        binder.forField(address).bind("address");
        binder.forField(rack).bind("rackNumber");
    }

    @Override
    protected void fillFields(LocationRead item) {
        if (item == null) return;
        address.setValue(item.address());
        rack.setValue(item.rackNumber());
    }

    @Override
    protected LocationCreate createDto() {
        return new LocationCreate(address.getValue(), rack.getValue());
    }

    @Override
    protected LocationUpdate updateDto(LocationRead item) {
        return new LocationUpdate(item.id(), address.getValue(), rack.getValue());
    }

}
