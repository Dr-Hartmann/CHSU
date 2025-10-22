package com.server;

import com.server.config.ServerConfig;
import com.server.net.ChatServer;

public class ServerApplication {
    static void main() {
        var config = ServerConfig.load();
        new ChatServer(config).start();
    }
}
