package com.client.base;

import com.client.service.NotificationService;
import com.vaadin.flow.component.button.Button;
import com.vaadin.flow.component.button.ButtonVariant;
import com.vaadin.flow.component.dialog.Dialog;
import com.vaadin.flow.component.grid.Grid;
import com.vaadin.flow.component.grid.GridVariant;
import com.vaadin.flow.component.icon.Icon;
import com.vaadin.flow.component.icon.VaadinIcon;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.textfield.TextField;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import org.springframework.web.client.HttpStatusCodeException;

import java.util.function.Consumer;

@RequiredArgsConstructor
public abstract class BaseViewManager<C, R, U> {

    @Getter
    private final BaseRestService<C, R, U> restService;
    private final NotificationService notificationService;
    private final Class<R> readClass;
    @Getter
    private final BaseViewDialog<C, R, U> dialog;

    protected abstract void configureColumns(Grid<R> grid);

    protected abstract Dialog createDialog(Consumer<C> handler);

    protected abstract Dialog updateDialog(R item, Consumer<U> handler);

    public Grid<R> initGrid() {
        var grid = new Grid<>(readClass, false);
        configureColumns(grid);

        grid.setEmptyStateText("Нет записей для вывода");
        grid.addThemeVariants(GridVariant.LUMO_NO_BORDER);

        grid.addComponentColumn(oldDto -> {
            var editBtn = new Button(new Icon(VaadinIcon.EDIT), _ ->
                    updateDialog(oldDto, newDto -> updateRecord(oldDto, newDto)).open());
            editBtn.addThemeVariants(ButtonVariant.LUMO_TERTIARY);

            var delBtn = new Button(new Icon(VaadinIcon.TRASH), _ -> deleteRecord(oldDto));
            delBtn.addThemeVariants(ButtonVariant.LUMO_ERROR, ButtonVariant.LUMO_TERTIARY);

            return new HorizontalLayout(editBtn, delBtn);
        }).setHeader("Действия").setWidth("150px").setFlexGrow(0).setFrozenToEnd(true);

        var dataProvider = restService.getDataProvider();
        dataProvider.refreshAll();
        grid.setDataProvider(dataProvider);
        return grid;
    }

    public Button initAddButton() {
        return new Button("Добавить...", new Icon(VaadinIcon.PLUS), _ ->
                createDialog(this::createRecord).open());
    }

    public abstract TextField initFilterField();

    private void createRecord(C dto) {
        try {
            restService.create(dto);
            notificationService.showSuccess("Сохранено");
        } catch (Exception e) {
            notificationService.showError(e);
        }
    }

    private void updateRecord(R oldDto, U newDto) {
        try {
            var id = restService.getEntityId(oldDto);
            restService.update(id, newDto);
            notificationService.showSuccess("Обновлено: " + id);
        } catch (Exception e) {
            notificationService.showError(e);
        }
    }

    private void deleteRecord(R dto) {
        try {
            var id = restService.getEntityId(dto);
            restService.delete(id);
            notificationService.showSuccess("Удалено: " + id);
        } catch (HttpStatusCodeException e) {
            if (e.getStatusCode().is5xxServerError()) {
                notificationService.showError(new IllegalArgumentException(
                        "Ошибка удаления: запись используется другими объектами или возникла системная ошибка."));
            } else if (e.getStatusCode().is4xxClientError()) {
                notificationService.showError(new IllegalArgumentException(
                        "Ошибка запроса: запись не найдена или доступ запрещен."));
            }
        } catch (Exception e) {
            notificationService.showError(e);
        }
    }

}
