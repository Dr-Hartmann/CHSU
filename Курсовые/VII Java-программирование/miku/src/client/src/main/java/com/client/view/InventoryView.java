package com.client.view;

import com.client.manager.InventoryManager;
import com.client.view.ui.ViewToolbar;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.dom.Style;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;

@Route("inventories")
@PageTitle("Инвентарь")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Инвентарь")
class InventoryView extends VerticalLayout {
    InventoryView(InventoryManager manager) {
        setSizeFull();
        setPadding(false);
        setSpacing(false);
        getStyle().setOverflow(Style.Overflow.HIDDEN);

        add(new ViewToolbar("Инвентарь"));
        add(new HorizontalLayout(manager.initAddButton(), manager.initFilterField()), manager.initGrid());
    }
}
