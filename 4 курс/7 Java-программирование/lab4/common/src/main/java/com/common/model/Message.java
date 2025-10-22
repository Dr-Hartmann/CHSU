package com.common.model;

import java.io.Serializable;
import java.time.LocalDateTime;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Message implements Serializable {
    private String username;
    private String text;
    private LocalDateTime time;
}
