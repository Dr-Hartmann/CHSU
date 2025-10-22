package com.example.application.base;

import org.springframework.stereotype.Service;
import com.vaadin.flow.component.UI;
import com.vaadin.flow.component.notification.Notification;
import com.vaadin.flow.component.notification.NotificationVariant;
import lombok.NoArgsConstructor;

@Service
@NoArgsConstructor
public class NotifiService {
    public void showError(UI ui, String text) {
        ui.access(() -> {
            var notification = Notification.show(text);
            notification.addThemeVariants(NotificationVariant.LUMO_ERROR);
            ui.push();
        });

    }

    public void showInfo(UI ui, String text) {
        ui.access(() -> {
            var notification = Notification.show(text);
            notification.addThemeVariants(NotificationVariant.LUMO_PRIMARY);
            ui.push();
        });
    }
}
