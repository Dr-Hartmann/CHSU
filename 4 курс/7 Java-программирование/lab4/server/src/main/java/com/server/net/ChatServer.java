package com.server.net;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.SocketException;
import java.time.LocalDateTime;
import java.util.Map;
import java.util.Scanner;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import lombok.RequiredArgsConstructor;

import com.common.model.Message;
import com.common.util.LoggerHandler;

import com.server.config.ServerConfig;

@RequiredArgsConstructor
public class ChatServer {
    private final ServerConfig config;
    private final ExecutorService pool = Executors.newCachedThreadPool();
    private final Map<String, ClientHandler> clients = new ConcurrentHashMap<>();
    private static final String SERVER_NAME = "[server]";

    private volatile boolean running = true;

    public void start() {
        try (var serverSocket = new ServerSocket(config.getPort())) {

            LoggerHandler.info("Сервер запущен на порту: " + config.getPort());
            startConsoleListener(serverSocket);

            while (running) {
                var socket = serverSocket.accept();
                var handler = new ClientHandler(socket, this);

                var username = handler.getUsername();
                if (username == null || username.isEmpty()) {
                    handler.close();
                    continue;
                }

                clients.put(username, handler);
                pool.submit(handler);

                broadcast(new Message(SERVER_NAME, "Прибыл - '" + username + "'", LocalDateTime.now()), handler);
                handler.send(
                        new Message(SERVER_NAME,
                                "Добро пожаловать в Сити-" + config.getPort() + ", " + username + ".",
                                LocalDateTime.now()));
            }

        } catch (SocketException e) {
            LoggerHandler.info("Сервер завершает работу по причине: " + e.toString());
        } catch (IOException | ClassNotFoundException e) {
            LoggerHandler.error(e);
        } finally {
            shutdown();
        }
    }

    public void broadcast(Message message, ClientHandler sender) {
        clients.values().stream()
                .filter(h -> h != sender)
                .forEach(h -> h.send(message));
    }

    public void remove(String username, ClientHandler sender) {
        if (username != null) {
            clients.remove(username);
            var msg = new Message(SERVER_NAME, "'" + username + "' пропал с радаров...", LocalDateTime.now());
            broadcast(msg, sender);
        }
    }

    private void shutdown() {
        running = false;
        clients.values().forEach(ClientHandler::close);
        pool.shutdownNow();
    }

    private void startConsoleListener(ServerSocket serverSocket) {
        new Thread(() -> {
            try (var scanner = new Scanner(System.in)) {
                while (running) {
                    if (scanner.nextLine().equalsIgnoreCase("exit")) {
                        running = false;
                        try {
                            if (!serverSocket.isClosed()) {
                                serverSocket.close();
                            }
                        } catch (IOException e) {
                            LoggerHandler.error(e);
                        }
                    }
                }
            }
        }, "ConsoleListener").start();
    }
}
