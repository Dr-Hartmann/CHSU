package com.example.application.lab.lab1;

import java.util.Arrays;
import com.example.application.base.ui.component.ViewToolbar;
import com.vaadin.flow.component.html.Div;
import com.vaadin.flow.component.html.ListItem;
import com.vaadin.flow.component.html.Main;
import com.vaadin.flow.component.html.Paragraph;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;
import com.vaadin.flow.theme.lumo.LumoUtility;

@Route("lab1")
@PageTitle("Lab 1")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Lab 1")
public class Lab1View extends Main {
        Lab1View() {
                setSizeFull();
                addClassNames(LumoUtility.BoxSizing.BORDER, LumoUtility.Display.FLEX, LumoUtility.FlexDirection.COLUMN,
                                LumoUtility.Padding.MEDIUM, LumoUtility.Gap.SMALL);

                var persons = new Customer(4);
                persons.getByIndex(0).setFirstname("Иван");
                persons.getByIndex(1).setFirstname("Егор");
                persons.getByIndex(2).setFirstname("Стёпа");
                persons.getByIndex(3).setFirstname("Анатолий");

                var t1 = new Paragraph("Список покупателей в алфавитном порядке:");

                var r2 = persons.printAndOrderByAll(SortingType.ALPHABETICALLY).split("\n");
                var t2 = new Paragraph();
                Arrays.asList(r2).forEach(x -> t2.add(x.equals("") ? new Paragraph("") : new ListItem(x)));

                var t3 = new Paragraph("Номер кредитной карточки находится в интервале 0-1000:");

                var r4 = persons.GetAllByCreditCardNumber(0, 1000).split("\n");
                var t4 = new Paragraph();
                Arrays.asList(r4).forEach(x -> t2.add(x.equals("") ? new Paragraph("") : new ListItem(x)));

                add(new ViewToolbar("Лабораторная работа 1"));
                add(new Div(t1, t2, t3, t4));
        }
}
