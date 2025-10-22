package com.client.net;

import java.io.ObjectInputStream;
import java.net.SocketException;
import com.common.model.Message;
import com.common.util.LoggerHandler;

import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
public class Receiver implements Runnable {
    private final ObjectInputStream in;
    private final ChatClient client;

    @Override
    public void run() {
        try {
            while (client.isRunning()) {
                var msg = (Message) in.readObject();
                LoggerHandler.printMessage(msg.getUsername() + ": " + msg.getText());
            }
        } catch (SocketException e) {
            LoggerHandler.info("Соединение закрыто: " + e.toString());
        } catch (Exception e) {
            LoggerHandler.error(e);
        }
        client.setRunning(false);
    }
}
