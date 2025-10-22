package com.example.lab1;

import java.awt.Color;
import lombok.AllArgsConstructor;

@AllArgsConstructor
public class IntIntColorTuple {
    private static final double SCALE = 4.0D;
    private static final double A = 100.0;

    private int centerX;
    private int centerY;
    private Color color;

    public IntIntColor getModifidedValues(double phi) {
        var r = A / Math.sqrt(phi);
        var x = r * Math.cos(phi);
        var y = r * Math.sin(phi);
        x = centerX + x * SCALE;
        y = centerY - y * SCALE;
        return new IntIntColor((int) x, (int) y, color);
    }

    public record IntIntColor(int x, int y, Color color) {
    }
}
