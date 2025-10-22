package com.example.lab2;

import java.util.List;

public class ParkingFill implements Runnable {
    private final List<Parking> areas;

    public ParkingFill(List<Parking> areas) {
        this.areas = areas;
        new Thread(this, this.getClass().getName()).start();
    }

    @Override
    public void run() {
        var i = 0;
        while (i < Parking.MAX_AUTOS) {
            for (var area : areas) {
                if (area.put(i)) {
                    ++i;
                    break;
                }
            }
        }
    }
}
