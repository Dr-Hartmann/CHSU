package com.client.net;

import java.io.Console;
import java.io.ObjectOutputStream;
import java.time.LocalDateTime;
import java.util.Scanner;

import com.common.model.Message;
import com.common.util.LoggerHandler;

import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
public class Sender implements Runnable {
    private final String username;
    private final ObjectOutputStream out;
    private final ChatClient client;

    private final Console console = System.console();
    private final Scanner scanner = console != null ? null : new Scanner(System.in);

    @Override
    public void run() {
        try {
            while (client.isRunning()) {
                var text = console != null ? console.readLine() : scanner.nextLine();
                if (text.isBlank())
                    continue;

                var msg = new Message(username, text, LocalDateTime.now());
                synchronized (out) {
                    out.writeObject(msg);
                    out.flush();
                }

                if ("exit".equalsIgnoreCase(text)) {
                    client.setRunning(false);
                }
            }
        } catch (Exception e) {
            LoggerHandler.error(e);
            client.setRunning(false);
        }
    }
}
