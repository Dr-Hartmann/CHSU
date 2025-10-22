package com.client.net;

import java.io.EOFException;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import com.client.config.ClientConfig;
import com.common.util.LoggerHandler;

import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

@RequiredArgsConstructor
public class ChatClient implements Runnable {
    private final ClientConfig config;
    private final String username;

    @Getter
    @Setter
    private volatile boolean running = true;

    public void start() {
        new Thread(this).start();
    }

    public void shutdown() {
        running = false;
    }

    @Override
    public void run() {
        try (var socket = new Socket(config.getHost(), config.getPort());
                var out = new ObjectOutputStream(socket.getOutputStream());
                var in = new ObjectInputStream(socket.getInputStream())) {

            out.writeObject(username);
            out.flush();

            var receiver = new Receiver(in, this);
            var receiverThread = new Thread(receiver);
            receiverThread.start();

            var sender = new Sender(username, out, this);
            var senderThread = new Thread(sender);
            senderThread.start();

            senderThread.join();
            receiverThread.join();

            LoggerHandler.info(username + " закончил общение.");
        } catch (EOFException e) {
            LoggerHandler.info(e.toString());
        } catch (IOException | InterruptedException e) {
            LoggerHandler.error(e);
            Thread.currentThread().interrupt();
        }
    }
}
