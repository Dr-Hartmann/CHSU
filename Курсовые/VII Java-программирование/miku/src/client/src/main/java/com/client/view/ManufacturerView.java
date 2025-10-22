package com.client.view;

import com.client.manager.ManufacturerManager;
import com.client.view.ui.ViewToolbar;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.dom.Style;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;

@Route("manufacturers")
@PageTitle("Производители")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Производители")
class ManufacturerView extends VerticalLayout {
    ManufacturerView(ManufacturerManager manager) {
        setSizeFull();
        setPadding(false);
        setSpacing(false);
        getStyle().setOverflow(Style.Overflow.HIDDEN);

        add(new ViewToolbar("Производители"));
        add(new HorizontalLayout(manager.initAddButton(), manager.initFilterField()), manager.initGrid());
    }
}
