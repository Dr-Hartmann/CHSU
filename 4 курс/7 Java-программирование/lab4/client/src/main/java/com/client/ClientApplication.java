package com.client;

import com.client.config.ClientConfig;
import com.client.net.ChatClient;
import com.common.util.LoggerHandler;

public class ClientApplication {
    static void main() {
        var console = System.console();
        if (console == null) {
            LoggerHandler.printMessage("Консольный режим недоступен...");
            return;
        }

        var username = "";
        while (true) {
            var input = console.readLine("Введите Ваш никнейм: ");
            if (!input.isBlank() && input.trim().length() >= 3) {
                username = input;
                break;
            }
            LoggerHandler.printMessage("Никнейм не должен быть пустым и должен быть длиннее 3 символов!");
        }

        var config = ClientConfig.load();
        new ChatClient(config, username).start();
    }
}
