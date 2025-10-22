package com.common.util;

import lombok.extern.slf4j.Slf4j;

@Slf4j
public class LoggerHandler {
    private LoggerHandler() {
    }

    public static void error(Exception e) {
        log.error("{}", e.toString());
    }

    public static void info(String message) {
        log.info("{}", message);
    }

    public static void printMessage(String message) {
        System.out.println(message);
        log.info("{}", message);
    }
}
