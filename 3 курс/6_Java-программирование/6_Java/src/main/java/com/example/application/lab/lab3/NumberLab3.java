package com.example.application.lab.lab3;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class NumberLab3 implements Comparable<NumberLab3> {
    private int id;
    private Double value;

    @Override
    public int compareTo(NumberLab3 o) {
        return value.compareTo(o.getValue());
    }
}
