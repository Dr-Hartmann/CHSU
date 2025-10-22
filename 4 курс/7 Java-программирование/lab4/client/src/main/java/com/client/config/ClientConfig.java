package com.client.config;

import com.common.util.PropertiesHandler;
import lombok.Getter;
import lombok.RequiredArgsConstructor;

@Getter
@RequiredArgsConstructor
public class ClientConfig {
    private final String host;
    private final int port;

    public static ClientConfig load() {
        var p = PropertiesHandler.load("client.properties");
        return new ClientConfig(
            p.getProperty("host"),
            Integer.parseInt(p.getProperty("port"))
        );
    }
}
