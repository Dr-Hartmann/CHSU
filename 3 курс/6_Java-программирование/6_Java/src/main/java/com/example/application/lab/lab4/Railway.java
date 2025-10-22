package com.example.application.lab.lab4;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.ToString;

@Data
@AllArgsConstructor
@ToString(includeFieldNames = true)
public class Railway {
    private String station;
    private int passengerPlaces;
    private int productPlaces;
}
