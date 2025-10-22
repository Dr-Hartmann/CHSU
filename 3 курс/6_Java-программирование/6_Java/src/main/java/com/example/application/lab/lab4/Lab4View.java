package com.example.application.lab.lab4;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

import org.apache.commons.io.FilenameUtils;

import com.example.application.base.ui.component.ViewToolbar;
import com.vaadin.flow.component.Component;
import com.vaadin.flow.component.button.Button;
import com.vaadin.flow.component.grid.Grid;
import com.vaadin.flow.component.html.Div;
import com.vaadin.flow.component.html.Main;
import com.vaadin.flow.component.html.NativeLabel;
import com.vaadin.flow.component.notification.Notification;
import com.vaadin.flow.component.notification.NotificationVariant;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.component.tabs.Tab;
import com.vaadin.flow.component.tabs.TabSheet;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;
import com.vaadin.flow.theme.lumo.LumoUtility;

@Route("lab4")
@PageTitle("Lab 4")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Lab 4")
public class Lab4View extends Main {
        private Tab[] tabs = new Tab[] {
                        new Tab("Таблица"),
                        new Tab("Кое-что о таблице"),
                        new Tab("Станция с наибольшим количеством проходящих через неё поездов"),
                        new Tab("Через какую из станций проходит больше пассажирских поездов"),
        };
        private Grid<Railway> grid = initGrid();
        private Component[] components = new Component[] {
                        getReadTable(),
                        getSizeTable(tabs[1]),
                        getAbsolutePowerfullStation(tabs[2]),
                        getMostPowerfullPassengersStation(),
        };

        Lab4View() {
                setSizeFull();
                addClassNames(LumoUtility.BoxSizing.BORDER, LumoUtility.Display.FLEX,
                                LumoUtility.FlexDirection.COLUMN, LumoUtility.Padding.MEDIUM,
                                LumoUtility.Gap.SMALL);
                add(getViewToolbar());
                add(getTabbedLayout());
        }

        private Component getReadTable() {
                var filePathField = new TextField("Путь к файлу:");
                filePathField.setValue("C:/Users/6_4lab.txt");
                var button1 = new Button("Чтение FileReader");
                var button2 = new Button("Чтение Files.lines()");
                button1.addClickListener(click -> input1(filePathField));
                button2.addClickListener(click -> input2(filePathField));
                return new VerticalLayout(filePathField, new Div(button1, button2), grid);
        }

        private Component getSizeTable(Tab tab) {
                var lbl = new NativeLabel("В таблице нет ничего");
                tab.getElement().addEventListener("click", event -> lbl
                                .setText("В таблице " + getGridData().size() + " строк"));
                return new VerticalLayout(lbl);
        }

        private Component getAbsolutePowerfullStation(Tab tab) {
                var lbl = new NativeLabel("В таблице нет ничего вообще");
                tab.getElement().addEventListener("click", event -> lbl.setText(getGridData()
                                .stream()
                                .max(Comparator.comparingInt(i -> i.getPassengerPlaces() + i.getProductPlaces()))
                                .map(Object::toString)
                                .orElse("Что-то не то")));
                return new VerticalLayout(lbl);
        }

        private Component getMostPowerfullPassengersStation() {
                var list = new NativeLabel("В таблице нет данных");
                var s1 = new TextField("Имя первой станции");
                var s2 = new TextField("Имя второй станции");
                var b = new Button("Вынести вердикт");
                b.addClickListener(event -> {
                        if (s1.getValue().equals("") || s2.getValue().equals("")) {
                                showNotificationError("Станции не найдены!");
                                return;
                        }
                        list.setText(getGridData()
                                        .stream()
                                        .filter(i -> i.getStation().contains(s1.getValue())
                                                        || i.getStation().contains(s2.getValue()))
                                        .max(Comparator.comparingInt(Railway::getPassengerPlaces))
                                        .map(Object::toString)
                                        .orElse("Ошибка"));
                });
                return new VerticalLayout(new Div(s1, s2), new Div(b), new Div(list));
        }

        private void input1(TextField filePathField) {
                var path = getPath(filePathField);
                List<Railway> rw = new ArrayList<>();
                try (BufferedReader br = new BufferedReader(new FileReader(path))) {
                        String line;
                        while ((line = br.readLine()) != null) {
                                var j = line.split(" ");
                                var station = j[0].substring(0, 20 <= j[0].length() ? 20 : j[0].length());
                                rw.add(new Railway(station, Integer.parseInt(j[1]), Integer.parseInt(j[2])));
                        }
                        grid.setItems(rw);
                } catch (RuntimeException | IOException e) {
                        showNotificationError(e.getMessage());
                }
        }

        private void input2(TextField filePathField) {
                var path = getPath(filePathField);
                List<Railway> rw = new ArrayList<>();
                try (var lines = Files.lines(Paths.get(path))) {
                        lines.forEach(s -> {
                                var j = s.split(" ");
                                rw.add(new Railway(j[0], Integer.parseInt(j[1]), Integer.parseInt(j[2])));
                        });
                        grid.setItems(rw);
                } catch (RuntimeException | IOException e) {
                        showNotificationError(e.getMessage());
                }
        }

        private String getPath(TextField filePathField) {
                var path = filePathField.getValue();
                if (path.equals("")) {
                        throw new FileFormatException("Путь пустой!");
                }

                if (!FilenameUtils.getExtension(path).equals("txt")) {
                        throw new FileFormatException("Загружен не текстовый файл!");
                }
                return path;
        }

        private List<Railway> getGridData() {
                return grid.getListDataView().getItems().toList();
        }

        private Component getViewToolbar() {
                var h1Title = new ViewToolbar("Лабораторная работа 4");
                h1Title.addClassName("lab4_green");
                return h1Title;
        }

        private Grid<Railway> initGrid() {
                var g = new Grid<>(Railway.class, false);
                g.addColumn(Railway::getStation)
                                .setHeader("Станция")
                                .setKey("name");        
                g.addColumn(Railway::getPassengerPlaces)
                                .setHeader("Пассажирских мест")
                                .setKey("passPlaces");
                g.addColumn(Railway::getProductPlaces)
                                .setHeader("Товарных мест");
                g.setEmptyStateText("Станций нет.");
                g.setAllRowsVisible(true);
                return g;
        }

        public VerticalLayout getTabbedLayout() {
                if (tabs.length != components.length) {
                        throw new IllegalArgumentException("Количество вкладок b контента должны совпадать.");
                }

                TabSheet tabSheet = new TabSheet();
                tabSheet.setSizeFull();

                for (int i = 0; i < tabs.length; ++i) {
                        tabSheet.add(tabs[i], components[i]);
                }

                VerticalLayout layout = new VerticalLayout(tabSheet);
                layout.setSizeFull();
                layout.setPadding(false);
                layout.setSpacing(false);
                return layout;
        }

        private void showNotificationError(String text) {
                Notification.show(text).addThemeVariants(NotificationVariant.LUMO_ERROR);
        }
}
