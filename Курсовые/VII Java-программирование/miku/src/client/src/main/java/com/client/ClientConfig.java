package com.client;

import lombok.Getter;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.client.RestClient;

@Configuration
public class ClientConfig {

    @Value("${SERVER_HOST:localhost}")
    private String host;

    @Value("${APP_SERVER_PORT:8080}")
    private String serverPort;

    @Value("${APP_CLIENT_PORT:5050}")
    @Getter
    private String clientPort;

    @Bean
    RestClient serverClient() {
        return RestClient.builder()
                .baseUrl("http://" + host + ":" + serverPort)
                .build();
    }

}
