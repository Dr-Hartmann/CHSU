package com.example.lab1;

import java.awt.Color;
import java.awt.Graphics;

import javax.swing.JPanel;

public class Picture extends JPanel {
    public void drawSome(int x, int y, Color color) {
        Graphics g = getGraphics();
        if (g != null) {
            try {
                g.setColor(color);
                g.fillOval(x, y, 4, 4);
            } finally {
                g.dispose();
            }
        }
    }
}
