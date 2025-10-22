package com.common.util;

import java.util.Properties;

public class PropertiesHandler {
    private PropertiesHandler() {
    }

    public static Properties load(String name) {
        var p = new Properties();
        try (var is = PropertiesHandler.class.getClassLoader().getResourceAsStream(name)) {
            p.load(is);
        } catch (Exception e) {
            LoggerHandler.error(e);
        }
        return p;
    }
}
