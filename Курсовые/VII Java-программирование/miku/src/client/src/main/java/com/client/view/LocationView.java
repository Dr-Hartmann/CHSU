package com.client.view;

import com.client.manager.LocationManager;
import com.client.view.ui.ViewToolbar;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.dom.Style;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;

@Route("locations")
@PageTitle("Расположение")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Расположение")
class LocationView extends VerticalLayout {
    LocationView(LocationManager manager) {
        setSizeFull();
        setPadding(false);
        setSpacing(false);
        getStyle().setOverflow(Style.Overflow.HIDDEN);

        add(new ViewToolbar("Расположение"));
        add(new HorizontalLayout(manager.initAddButton(), manager.initFilterField()), manager.initGrid());
    }
}
