package com.client.view.ui;

import com.client.view.MainView;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.UI;
import com.vaadin.flow.component.applayout.AppLayout;
import com.vaadin.flow.component.button.Button;
import com.vaadin.flow.component.html.Span;
import com.vaadin.flow.component.icon.Icon;
import com.vaadin.flow.component.icon.VaadinIcon;
import com.vaadin.flow.component.orderedlayout.FlexComponent;
import com.vaadin.flow.component.orderedlayout.Scroller;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.component.sidenav.SideNav;
import com.vaadin.flow.component.sidenav.SideNavItem;
import com.vaadin.flow.router.Layout;
import com.vaadin.flow.server.VaadinSession;
import com.vaadin.flow.server.menu.MenuConfiguration;
import com.vaadin.flow.server.menu.MenuEntry;
import com.vaadin.flow.theme.lumo.LumoUtility;

@Layout
public final class MainLayout extends AppLayout {

    private static final String SESSION_THEME_KEY = "selectedTheme";

    public MainLayout() {
        setPrimarySection(Section.DRAWER);
        addToDrawer(createHeader(), new Scroller(createSideNav()));

        var theme = VaadinSession.getCurrent().getAttribute(SESSION_THEME_KEY);
        if ("matrix".equals(theme)) {
            UI.getCurrent().getElement().executeJs("document.documentElement.classList.add('theme-matrix')");
        }
    }

    private Component createHeader() {
        var appLogo = VaadinIcon.TERMINAL.create();
        appLogo.setSize("80px");

        var logoLink = new com.vaadin.flow.router.RouterLink();
        logoLink.setRoute(MainView.class);
        logoLink.add(appLogo);
        logoLink.getStyle().set("cursor", "pointer");
        logoLink.getStyle().set("text-decoration", "none");

        var appName = new Span("SYSTEM_MUKUTSKIKH_V7");
        appName.addClassNames("matrix-title", "terminal-cursor");

        var subTitle = new Span("CPU_HARDWARE_UNIT_OS");
        subTitle.addClassName("matrix-subtitle");

        var themeButton = createThemeToggleButton();

        var header = new VerticalLayout(logoLink, appName, subTitle, themeButton);
        header.setAlignItems(FlexComponent.Alignment.CENTER);
        header.setSpacing(false);
        header.setPadding(true);
        header.setWidthFull();
        header.setMaxWidth("100%");
        header.addClassName("drawer-header");

        return header;
    }

    private Component createSideNav() {
        var nav = new SideNav();
        nav.setWidthFull();
        nav.addClassNames(LumoUtility.Margin.Horizontal.MEDIUM);

        MenuConfiguration.getMenuEntries()
                .forEach(entry -> nav.addItem(createSideNavItem(entry)));

        var scroller = new Scroller(nav);
        scroller.setClassName(LumoUtility.Padding.SMALL);
        return scroller;
    }

    private SideNavItem createSideNavItem(MenuEntry entry) {
        return entry.icon() != null
                ? new SideNavItem(entry.title(), entry.path(), new Icon(entry.icon()))
                : new SideNavItem(entry.title(), entry.path());
    }

    private Component createThemeToggleButton() {
        var button = new Button("Сменить протокол", VaadinIcon.PALETTE.create());
        var themeCondition = """
                const html = document.documentElement;
                if (html.classList.contains('theme-matrix')) {
                  html.classList.remove('theme-matrix');
                  return false;
                } else {
                  html.classList.add('theme-matrix');
                  return true;
                }
                """;
        button.addClickListener(_ ->
                UI.getCurrent().getElement().executeJs(themeCondition)
                        .then(Boolean.class, isMatrix ->
                                VaadinSession.getCurrent().setAttribute(SESSION_THEME_KEY, isMatrix ? "matrix" : "")));

        return button;
    }

}
