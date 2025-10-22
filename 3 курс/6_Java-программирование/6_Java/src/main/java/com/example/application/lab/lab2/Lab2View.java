package com.example.application.lab.lab2;

import java.util.Arrays;
import java.util.List;

import com.example.application.base.ui.component.ViewToolbar;
import com.vaadin.flow.component.html.Div;
import com.vaadin.flow.component.html.Main;
import com.vaadin.flow.component.html.Paragraph;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;
import com.vaadin.flow.theme.lumo.LumoUtility;

@Route("lab2")
@PageTitle("Lab 2")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Lab 2")
public class Lab2View extends Main {
        Lab2View() {
                setSizeFull();
                addClassNames(LumoUtility.BoxSizing.BORDER, LumoUtility.Display.FLEX, LumoUtility.FlexDirection.COLUMN,
                                LumoUtility.Padding.MEDIUM, LumoUtility.Gap.SMALL);

                var contentDiv = new Div(
                                new Paragraph("/// Лабораторная 2.1"), lb21(),
                                new Paragraph("/// Лабораторная 2.2"), lb22(),
                                new Paragraph("/// Лабораторная 2.3"), lb23());

                add(new ViewToolbar("Лабораторная работа 2"));
                add(contentDiv);
        }

        private Div lb21() {
                var contentDiv = new Div();
                // Создать объект-контейнер в соответствии с вариантом задания (Vector)
                // и заполнить его данными, тип которых определяется вариантом задания (char).
                Container1 cont = new Container1();
                cont.fillData('1', '2', 'O', '3');

                Arrays.asList(cont.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                // Изменить контейнер, удалив из него одни элементы и заменив другие.
                cont.remove(1, 1);
                cont.replaceElementAt(0, 'G');
                cont.replaceElementAt(2, 'S');
                // Просмотреть контейнер, используя для доступа к его элементам итераторы.
                Arrays.asList(cont.viewDataIterator().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                // Создать второй контейнер этого же класса и заполнить его данными
                // того же типа, что и первый контейнер.
                Container1 container12 = new Container1('D', 'O', 'O', 'M');
                Arrays.asList(container12.viewDataIterator().split("\n"))
                                .forEach(x -> contentDiv.add(new Paragraph(x)));

                // Изменить первый контейнер, удалив из него n элементов после заданного
                // и добавив затем в него все элементы из второго контейнера.
                cont.remove(0, 0);
                cont.fillAllFromContainer(container12);
                Arrays.asList(cont.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                return contentDiv;
        }

        private Div lb22() {
                var contentDiv = new Div();

                // выполнить то же самое, но для данных пользовательского типа.
                // Создать объект-контейнер в соответствии с вариантом задания (Vector)
                // и заполнить его данными, тип которых определяется вариантом задания (char).
                Container2<Workshop> container21 = new Container2<>(List.of(
                                new Workshop("Деньгоотжимальный", "Мордашёв", 1),
                                new Workshop("Газовый", "А. художник", 8841),
                                new Workshop("Русский мир", "Шаман", 26 + 15 + 22)));

                // // Просмотреть контейнер.
                contentDiv.add(new Div("-------------------------------------"));
                Arrays.asList(container21.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                // // Изменить контейнер, удалив из него одни элементы и заменив другие.
                container21.remove(1, 1);
                container21.replaceElementAt(0, new Workshop("Влад", "Джанго", 68));

                // Просмотреть контейнер, используя для доступа к его элементам итераторы.
                contentDiv.add(new Div("-------------------------------------"));
                Arrays.asList(container21.viewDataIterator().split("\n"))
                                .forEach(x -> contentDiv.add(new Paragraph(x)));

                // Создать второй контейнер этого же класса и заполнить его данными
                // того же типа, что и первый контейнер.
                Container2<Workshop> container22 = new Container2<>(List.of(
                                new Workshop("Билли", "Данжен-Мастер", 13)));

                // Изменить первый контейнер, удалив из него n элементов после заданного
                // и добавив затем в него все элементы из второго контейнера.
                container21.remove(0, 2);
                container21.fillAllFromContainer(container22);
                contentDiv.add(new Div("-------------------------------------"));
                Arrays.asList(container22.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                return contentDiv;
        }

        private Div lb23() {
                var contentDiv = new Div();

                // Создать контейнер, содержащий объекты пользовательского типа.
                // Тип контейнера выбирается в соответствии с вариантом задания (Vector).
                Container3<Workshop> container = new Container3<>(List.of(
                                new Workshop("Стёпа", "Тараканы", 4),
                                new Workshop("Вася", "Шут гороховый", 0),
                                new Workshop("Полина", "Аня", 24),
                                new Workshop("Никита", "ChatGPT 4o", 1),
                                new Workshop("Подчинённый", "Шеф", 15)));

                container.orderByName();
                contentDiv.add(new Div("// Отсортировать его по возрастанию элементов (по имени)"));
                Arrays.asList(container.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                // Используя подходящий алгоритм, найти в контейнере элемент,
                // удовлетворяющий заданному условию.
                contentDiv.add(new Div("-------------------------------------"));
                Arrays.asList(container.getByNumberOfWorkersInRange(10, 24).toString().split("\n"))
                                .forEach(x -> contentDiv.add(new Paragraph(x)));

                // Переместить элементы, удовлетворяющие заданному условию в другой
                // (предварительно пустой) контейнер.
                // Тип второго контейнера определяется вариантом задания (TreeSet).
                Container4<Workshop> treeSet = new Container4<>();
                treeSet.fillAllFromVector(container.extractByNumberOfWorkersInRange(0, 6));

                contentDiv.add(new Div("// Просмотреть второй контейнер."));
                Arrays.asList(treeSet.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                contentDiv.add(new Div("// Просмотреть первый контейнер."));
                Arrays.asList(container.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                // Отсортировать первый и второй контейнеры по убыванию элементов.
                // Просмотреть их.
                treeSet.orderByDescending();
                contentDiv.add(new Div("// Просмотреть второй контейнер."));
                Arrays.asList(treeSet.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                container.orderByNameReverse();
                contentDiv.add(new Div("// Просмотреть первый контейнер."));
                Arrays.asList(container.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                // Получить третий контейнер путем слияния первых двух.
                // Тип третьего контейнера определить самостоятельно.
                Container4<Workshop> cont3 = new Container4<>();
                cont3.concat(treeSet.getArray(), container.getArray());
                contentDiv.add(new Div("// Просмотреть третий контейнер."));
                Arrays.asList(cont3.viewData().split("\n")).forEach(x -> contentDiv.add(new Paragraph(x)));

                return contentDiv;
        }

}
