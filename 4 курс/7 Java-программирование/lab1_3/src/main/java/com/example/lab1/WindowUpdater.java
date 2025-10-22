package com.example.lab1;

import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import java.awt.Color;
import javax.swing.JFrame;
import static javax.swing.JFrame.EXIT_ON_CLOSE;

@Slf4j
@AllArgsConstructor
public class WindowUpdater implements Runnable {
    private IntIntColorTuple value;
    private Picture pane;

    private static final double STEP = 0.01;
    private static int locX = 0;
    private static int locY = 0;

    @Override
    public void run() {
        log.info(Thread.currentThread().getName());
        double phi = 0.01;
        // synchronized (this) {
        while (phi < 128 * Math.PI) {
            // try {
            var input = value.getModifidedValues(phi);
            phi += STEP;
            pane.drawSome(input.x(), input.y(), input.color());

            // if (input.color() == Color.RED) {
            // Thread.sleep(1L);
            // } else if (input.color() == Color.GREEN) {
            // Thread.sleep(3L);
            // } else {
            // Thread.sleep(7L);
            // }
            // } catch (InterruptedException e) {
            // logger.warn(e.toString());
            // Thread.currentThread().interrupt();
            // }
        }
        // }
    }

    public static void start(int x, int y, Color color) {
        var window = getWindow();
        window.setLocation(locX, locY);
        locX += 300;
        locY += 100;
        var pane = new Picture();
        window.setContentPane(pane);

        var th = new WindowUpdater(new IntIntColorTuple(x, y, color), pane);
        (new Thread(th)).start();
    }

    public static void start(int x, int y, Color color, Picture pane) {
        var th = new WindowUpdater(new IntIntColorTuple(x, y, color), pane);
        (new Thread(th)).start();
    }

    private static JFrame getWindow() {
        var window = new JFrame(Thread.currentThread().toString());
        window.setVisible(true);
        window.setSize(1200, 800);
        // window.setResizable(false);
        window.setBackground(Color.WHITE);
        window.setDefaultCloseOperation(EXIT_ON_CLOSE);
        return window;
    }

    public static void case1() {
        var window = getWindow();
        var pane = new Picture();
        window.setContentPane(pane);

        start(220, 350, Color.RED, pane);
        start(240, 350, Color.GREEN, pane);
        start(260, 350, Color.BLUE, pane);
    }

    public static void case2() {
        start(200, 150, Color.RED);
        start(350, 250, Color.GREEN);
        start(500, 350, Color.BLUE);
    }
}
