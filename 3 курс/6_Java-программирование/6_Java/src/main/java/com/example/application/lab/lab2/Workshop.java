package com.example.application.lab.lab2;

import lombok.Data;
import lombok.AllArgsConstructor;

@Data
@AllArgsConstructor
public class Workshop implements IWorkshop, Comparable<Workshop> {
    private String name;
    private String chief;
    private int numberOfWorkers;

    @Override
    public int compareTo(Workshop other) {
        return this.name.compareTo(other.name);
    }

    @Override
    public String toString() {
        return String.format("Цех: %s,\nНачальник: %s,\nКоличество работающих: %s.", name, chief, numberOfWorkers);
    }
}
