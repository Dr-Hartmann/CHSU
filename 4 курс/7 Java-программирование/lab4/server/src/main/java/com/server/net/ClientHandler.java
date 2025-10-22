package com.server.net;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;

import com.common.model.Message;
import com.common.util.LoggerHandler;

import lombok.Getter;

public class ClientHandler implements Runnable {
    private final Socket socket;
    private final ChatServer server;
    private final ObjectOutputStream out;
    private final ObjectInputStream in;

    @Getter
    private String username;

    public ClientHandler(Socket socket, ChatServer server) throws IOException, ClassNotFoundException {
        this.socket = socket;
        this.server = server;
        out = new ObjectOutputStream(this.socket.getOutputStream());
        out.flush();
        in = new ObjectInputStream(this.socket.getInputStream());
        username = (String) in.readObject();
    }

    @Override
    public void run() {
        try (socket;) {
            while (true) {
                var msg = (Message) in.readObject();
                if (msg == null || msg.getText() == null || "exit".equals(msg.getText())) {
                    break;
                }
                server.broadcast(msg, this);
            }
        } catch (IOException | ClassNotFoundException e) {
            LoggerHandler.error(e);
        } finally {
            server.remove(username, this);
            close();
        }
    }

    public synchronized void send(Message message) {
        try {
            out.writeObject(message);
            out.flush();
        } catch (Exception e) {
            LoggerHandler.error(e);
        }
    }

    public void close() {
        try {
            if (!socket.isClosed()) {
                socket.close();
            }
        } catch (IOException e) {
            LoggerHandler.error(e);
        }
    }
}
