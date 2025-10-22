package com.server.config;

import com.common.util.PropertiesHandler;
import lombok.Getter;
import lombok.RequiredArgsConstructor;

@Getter
@RequiredArgsConstructor
public class ServerConfig {
    private final int port;

    public static ServerConfig load() {
        var p = PropertiesHandler.load("server.properties");
        return new ServerConfig(Integer.parseInt(p.getProperty("port")));
    }
}
