package com.example.application.lab.lab4;

public class FileFormatException extends RuntimeException {
    public FileFormatException(String message, Object... params) {
        super(String.format(message, params));
    }
}
