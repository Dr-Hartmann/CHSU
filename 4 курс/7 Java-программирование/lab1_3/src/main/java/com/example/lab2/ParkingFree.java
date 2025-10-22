package com.example.lab2;

import java.util.List;

public class ParkingFree implements Runnable {
    private final List<Parking> areas;

    public ParkingFree(List<Parking> areas) {
        this.areas = areas;
        new Thread(this, this.getClass().getName()).start();
    }

    @Override
    public void run() {
        var i = 0;
        while (i < Parking.MAX_AUTOS) {
            for (var area : areas) {
                if (area.get()) {
                    ++i;
                    break;
                }
            }
        }
    }
}
