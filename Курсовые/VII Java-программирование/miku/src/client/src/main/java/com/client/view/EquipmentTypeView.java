package com.client.view;

import com.client.manager.EquipmentTypeManager;
import com.client.view.ui.ViewToolbar;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.dom.Style;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;

@Route("equipments-types")
@PageTitle("Тип оборудования")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Тип оборудования")
class EquipmentTypeView extends VerticalLayout {
    EquipmentTypeView(EquipmentTypeManager manager) {
        setSizeFull();
        setPadding(false);
        setSpacing(false);
        getStyle().setOverflow(Style.Overflow.HIDDEN);

        add(new ViewToolbar("Типы оборудования"));
        add(new HorizontalLayout(manager.initAddButton(), manager.initFilterField()), manager.initGrid());
    }
}
