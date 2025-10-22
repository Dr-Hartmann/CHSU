package com.client.service;

import org.jspecify.annotations.NonNull;
import org.springframework.stereotype.Service;
import com.vaadin.flow.component.notification.Notification;
import com.vaadin.flow.component.notification.NotificationVariant;

@Service
public class NotificationService {

    public void showSuccess(String message) {
        Notification.show(message, 5000, Notification.Position.TOP_CENTER)
                .addThemeVariants(NotificationVariant.LUMO_SUCCESS);
    }

    public void showError(@NonNull Exception e) {
        Notification.show(e.getMessage(), 10000, Notification.Position.BOTTOM_CENTER)
                .addThemeVariants(NotificationVariant.LUMO_ERROR);
    }

}
