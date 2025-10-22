package com.example.application.lab.lab3;

import com.example.application.base.ui.component.ViewToolbar;
import com.vaadin.flow.component.Key;
import com.vaadin.flow.component.button.Button;
import com.vaadin.flow.component.grid.Grid;
import com.vaadin.flow.component.html.Div;
import com.vaadin.flow.component.html.H1;
import com.vaadin.flow.component.html.Main;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;
import com.vaadin.flow.theme.lumo.LumoUtility;

@Route("lab3")
@PageTitle("Lab 3")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Lab 3")
public class Lab3View extends Main {
        private static final Array select = new Selection();
        private static final IArray insert = new Insert();

        TextField tfInputSelection = new TextField(
                        "Введите число (Selection, логарифм)");
        TextField tfInputInsert = new TextField(
                        "Введите число (Insert, возведение в квадрат)");

        Grid<NumberLab3> gridOutputSelection = new Grid<>();
        Grid<NumberLab3> gridOutputInsert = new Grid<>();

        Button bInput = new Button("Ввод ('E')");
        Button bOutput = new Button("Показать всё (пробел)");

        Lab3View() {
                setSizeFull();
                addClassNames(LumoUtility.BoxSizing.BORDER, LumoUtility.Display.FLEX,
                                LumoUtility.FlexDirection.COLUMN,
                                LumoUtility.Padding.MEDIUM,
                                LumoUtility.Gap.SMALL);
                bInput.addClickListener(click -> input());
                bInput.addClickShortcut(Key.KEY_E);
                bOutput.addClickListener(click -> output());
                bOutput.addClickShortcut(Key.SPACE);

                final String pattern = "[+-]?[0-9]+";
                tfInputSelection.setPattern(pattern);
                tfInputSelection.setAllowedCharPattern(pattern);
                tfInputSelection.setErrorMessage("Неверный ввод!!!");

                tfInputInsert.setPattern(pattern);
                tfInputInsert.setAllowedCharPattern(pattern);
                tfInputInsert.setErrorMessage("Неверный ввод!!!");

                gridOutputSelection.addColumn(NumberLab3::getId)
                                .setHeader("ID");
                gridOutputSelection.addColumn(NumberLab3::getValue)
                                .setHeader("Value");

                gridOutputInsert.addColumn(NumberLab3::getId)
                                .setHeader("ID");
                gridOutputInsert.addColumn(NumberLab3::getValue)
                                .setHeader("Value");

                var contentDiv = new Div();

                contentDiv.add(new H1("Лабораторная работа №3"),
                                new HorizontalLayout(bInput, bOutput),
                                tfInputSelection,
                                gridOutputSelection,
                                tfInputInsert,
                                gridOutputInsert);

                add(new ViewToolbar("Лабораторная работа 3"));
                add(contentDiv);
        }

        private void input() {
                if (!tfInputSelection.isEmpty()) {
                        select.add(Double.valueOf(tfInputSelection.getValue()));
                        tfInputSelection.clear();
                }
                if (!tfInputInsert.isEmpty()) {
                        insert.add(Double.valueOf(tfInputInsert.getValue()));
                        tfInputInsert.clear();
                }
        }

        private void output() {
                select.sort();
                insert.sort();
                gridOutputSelection.setItems(select.getAll());
                gridOutputInsert.setItems(insert.getAll());
        }
}
