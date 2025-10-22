package com.client.view;

import com.client.manager.EquipmentManager;
import com.client.view.ui.ViewToolbar;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.dom.Style;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;

@Route("equipments")
@PageTitle("Оборудование")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Оборудование")
class EquipmentView extends VerticalLayout {
    EquipmentView(EquipmentManager manager) {
        setSizeFull();
        setPadding(false);
        setSpacing(false);
        getStyle().setOverflow(Style.Overflow.HIDDEN);

        add(new ViewToolbar("Оборудование"));
        add(new HorizontalLayout(manager.initAddButton(), manager.initFilterField()), manager.initGrid());
    }
}
