package com.client.base;

import com.client.service.NotificationService;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.button.Button;
import com.vaadin.flow.component.button.ButtonVariant;
import com.vaadin.flow.component.dialog.Dialog;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.data.binder.BeanValidationBinder;
import com.vaadin.flow.data.binder.Binder;
import lombok.RequiredArgsConstructor;

import java.util.function.Consumer;
import java.util.function.Supplier;

@RequiredArgsConstructor
public abstract class BaseViewDialog<C, R, U> {

    protected final NotificationService notificationService;

    protected abstract Component[] fields();

    protected abstract void bindCreate(BeanValidationBinder<C> binder);

    protected abstract void bindUpdate(BeanValidationBinder<U> binder);

    protected abstract void fillFields(R item);

    protected abstract C createDto();

    protected abstract U updateDto(R item);

    public Dialog createDialog(String title, Class<C> createClass, Consumer<C> handler) {
        try {
            var binder = new BeanValidationBinder<>(createClass);
            bindCreate(binder);
            return getDialog(notificationService, title, binder, this::createDto, handler, fields());
        } catch (Exception e) {
            notificationService.showError(e);
        }
        return null;
    }

    public Dialog updateDialog(String title, Class<U> updateClass, R item, Consumer<U> handler) {
        try {
            var binder = new BeanValidationBinder<>(updateClass);
            bindUpdate(binder);
            fillFields(item);
            return getDialog(notificationService, title, binder, () -> updateDto(item), handler, fields());
        } catch (Exception e) {
            notificationService.showError(e);
        }
        return null;
    }

    private static <T> Dialog getDialog(NotificationService notificationService, String title, Binder<T> binder,
                                        Supplier<T> dtoFactory, Consumer<T> saveHandler, Component... fields) {
        var dialog = new Dialog();
        dialog.setHeaderTitle(title);

        var saveBtn = new Button("Сохранить", _ -> {
            try {
                if (binder.validate().isOk()) {
                    var dto = dtoFactory.get();
                    saveHandler.accept(dto);
                    dialog.close();
                }
            } catch (Exception e) {
                notificationService.showError(e);
            }
        });
        saveBtn.addThemeVariants(ButtonVariant.LUMO_PRIMARY);

        var cancelBtn = new Button("Отмена", _ -> dialog.close());

        dialog.add(new VerticalLayout(fields));
        dialog.getFooter().add(cancelBtn, saveBtn);
        return dialog;
    }

}
