package com.client.dashboard;

import com.client.ClientConfig;
import com.client.service.NotificationService;
import com.google.zxing.BarcodeFormat;
import com.google.zxing.WriterException;
import com.google.zxing.client.j2se.MatrixToImageWriter;
import com.google.zxing.qrcode.QRCodeWriter;
import com.vaadin.flow.component.UI;
import com.vaadin.flow.component.button.Button;
import com.vaadin.flow.component.html.Anchor;
import com.vaadin.flow.component.html.Image;
import com.vaadin.flow.component.html.Span;
import com.vaadin.flow.component.icon.VaadinIcon;
import com.vaadin.flow.component.orderedlayout.FlexComponent;
import com.vaadin.flow.component.orderedlayout.HorizontalLayout;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;
import com.vaadin.flow.server.streams.DownloadHandler;

import java.net.InetAddress;

public class QrCodeCurrentLink extends VerticalLayout {

    private final transient NotificationService notificationService;

    public QrCodeCurrentLink(NotificationService notificationService, ClientConfig clientConfig) {
        this.notificationService = notificationService;

        try {
            var localIp = InetAddress.getLocalHost().getHostAddress();
            var currentPath = UI.getCurrent().getInternals().getActiveViewLocation().getPath();
            var fullUrl = "http://" + localIp + ":" + clientConfig.getClientPort() + "/" + currentPath;

            var link = new Anchor(fullUrl, fullUrl);
            var copyButton = new Button(VaadinIcon.COPY.create());

            copyButton.addClickListener(e -> {
                UI.getCurrent().getPage().executeJs(
                        "navigator.clipboard.writeText($0).then(() => { return true; });",
                        fullUrl);
                notificationService.showSuccess("Ссылка скопирована!");
            });

            var qrImage = new Image();
            qrImage.setSrc(generateQrCode(link.getText()));
            var layout = new VerticalLayout(new HorizontalLayout(new Span("Поделиться: "), link, copyButton), qrImage);
            layout.setDefaultHorizontalComponentAlignment(FlexComponent.Alignment.CENTER);
            add(layout);

        } catch (Exception e) {
            add(new Span("Ошибка определения IP"));
            notificationService.showError(e);
        }
    }

    private DownloadHandler generateQrCode(String text) {
        return downloadEvent -> {
            try (var out = downloadEvent.getOutputStream()) {
                var qrWriter = new QRCodeWriter();
                var bitMatrix = qrWriter.encode(text, BarcodeFormat.QR_CODE, 300, 300);
                downloadEvent.setContentType("image/png");
                MatrixToImageWriter.writeToStream(bitMatrix, "PNG", out);
            } catch (WriterException e) {
                notificationService.showError(e);
            }
        };
    }

}
