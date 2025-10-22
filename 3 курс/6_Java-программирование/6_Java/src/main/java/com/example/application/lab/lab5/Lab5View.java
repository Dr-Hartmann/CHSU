package com.example.application.lab.lab5;

import java.io.ByteArrayInputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.concurrent.CompletableFuture;
import com.example.application.base.NotifiService;
import com.example.application.base.ui.component.ViewToolbar;
import com.vaadin.flow.component.UI;
import com.vaadin.flow.component.button.Button;
import com.vaadin.flow.component.html.Main;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.component.tabs.Tab;
import com.vaadin.flow.component.tabs.TabSheet;
import com.vaadin.flow.component.textfield.IntegerField;
import com.vaadin.flow.component.textfield.TextArea;
import com.vaadin.flow.component.textfield.TextField;
import com.vaadin.flow.component.upload.Upload;
import com.vaadin.flow.router.Menu;
import com.vaadin.flow.router.PageTitle;
import com.vaadin.flow.router.Route;
import com.vaadin.flow.server.streams.UploadHandler;
import com.vaadin.flow.theme.lumo.LumoUtility;

@Route("lab5")
@PageTitle("Lab 5")
@Menu(order = 0, icon = "vaadin:clipboard-check", title = "Lab 5")
public class Lab5View extends Main {
        private final transient BinGeneratorService binGenService;
        private final transient NotifiService notifiService;
        private final transient UniqueNumbersService unService;
        private final UI ui = com.vaadin.flow.component.UI.getCurrent();

        private TextArea taOut = initOutTextArea("Содержимое файла...");
        private TextArea taBinOut = initOutTextArea("Уникальные числа из нечётных цифр в файле...");
        private IntegerField nfInputSize = initInputSizeNumberField();
        private IntegerField nfInputMaxValue = initInputValue("Максимальное значение");
        private IntegerField nfInputMinValue = initInputValue("Минимальное значение");
        private TextField tfFileName = new TextField("Имя файла");
        private Button bCreate = initButonCreate();
        private Upload bUpload = initUploadButton();

        public Lab5View(BinGeneratorService sBinGenerator, NotifiService sNotifi, UniqueNumbersService sUniqueNumbers) {
                this.binGenService = sBinGenerator;
                this.notifiService = sNotifi;
                this.unService = sUniqueNumbers;

                setSizeFull();
                addClassNames(LumoUtility.BoxSizing.BORDER, LumoUtility.Display.FLEX,
                                LumoUtility.FlexDirection.COLUMN, LumoUtility.Padding.MEDIUM, LumoUtility.Gap.SMALL);
                add(new ViewToolbar("Лабораторная работа 5"));
                add(getTabbedLayout());
        }

        private VerticalLayout getTabbedLayout() {
                var tabSheet = new TabSheet();
                var createTabContent = new VerticalLayout(tfFileName,
                                new HorizontalLayout(nfInputSize, nfInputMaxValue, nfInputMinValue), bCreate);
                var uploadTabContent = new VerticalLayout(bUpload, taBinOut);
                tabSheet.setSizeFull();
                tabSheet.add(new Tab("Создание"), createTabContent);
                tabSheet.add(new Tab("Вывод"), uploadTabContent);
                return new VerticalLayout(tabSheet, taOut);
        }

        private TextArea initOutTextArea(String text) {
                var out = new TextArea(text);
                out.setSizeFull();
                out.setReadOnly(true);
                return out;
        }

        private IntegerField initInputSizeNumberField() {
                var out = new IntegerField("Количество чисел");
                out.setMin(1);
                out.setStepButtonsVisible(true);
                return out;
        }

        private IntegerField initInputValue(String text) {
                var out = new IntegerField(text);
                out.setStepButtonsVisible(true);
                return out;
        }

        private Button initButonCreate() {
                var out = new Button("Сгенерировать целочисленный бинарный файл");
                out.addClickListener(click -> {
                        var name = tfFileName.getValue();
                        var size = nfInputSize.getValue();
                        var max = nfInputMaxValue.getValue();
                        var min = nfInputMinValue.getValue();

                        notifiService.showInfo(ui, "Идёт генерация файла '" + name + "'...");
                        CompletableFuture
                                        .supplyAsync(() -> binGenService.generateFileAsync(name, size, max, min))
                                        .thenAccept(fileName -> ui.access(() -> {
                                                taOut.clear();
                                                try (var fis = new FileInputStream(fileName)) {
                                                        var result = unService.getNumbers(fis);
                                                        taOut.setValue(result);
                                                        notifiService.showInfo(ui, "Файл " + fileName + " создан.");
                                                } catch (IOException e) {
                                                        notifiService.showError(ui, "Ошибка чтения: " + e.getMessage());
                                                }
                                        })).exceptionally(e -> {
                                                notifiService.showError(ui, e.getMessage());
                                                return null;
                                        });
                });
                return out;
        }

        private Upload initUploadButton() {
                var out = new Upload(uploadHandler());
                out.setWidthFull();
                out.setMaxFiles(1);
                out.setAutoUpload(true);
                out.setDropAllowed(true);
                out.addFileRemovedListener(e -> notifiService.showInfo(ui, "Загрузка отменена."));
                out.addAllFinishedListener(e -> notifiService.showInfo(ui, "Загрузка завершена."));
                return out;
        }

        private UploadHandler uploadHandler() {
                return event -> ui.access(() -> {
                        taBinOut.clear();
                        try (var in = event.getInputStream()) {
                                var bytes = in.readAllBytes();
                                taOut.setValue(unService.getNumbers(new ByteArrayInputStream(bytes)));
                                taBinOut.setValue(unService.getNumbersFromOddDigits(new ByteArrayInputStream(bytes)));
                        } catch (IOException e) {
                                notifiService.showError(ui, "Ошибка чтения: " + e.getMessage());
                        }
                });
        }
}
