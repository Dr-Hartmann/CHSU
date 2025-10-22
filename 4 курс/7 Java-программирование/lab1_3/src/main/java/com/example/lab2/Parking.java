package com.example.lab2;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import java.util.concurrent.Semaphore;
import java.util.concurrent.ThreadLocalRandom;
import java.util.concurrent.TimeUnit;

public class Parking {
    private static final Logger logger = LoggerFactory.getLogger(Parking.class);

    private final Semaphore semAutoPlaces = new Semaphore(0);
    private final Semaphore semParckingSize;
    private final String name;

    public static final int MAX_AUTOS = 15;
    public final int maxPlaces;

    public Parking(String name, int maxPlaces) {
        this.name = name;
        this.maxPlaces = maxPlaces;
        semParckingSize = new Semaphore(maxPlaces);
        logger.debug("[{}] Открыта стоянка на {} мест", this.name, this.maxPlaces);
    }

    public synchronized boolean put(int auto) {
        var startTime = System.currentTimeMillis();
        boolean isParked = false;
        try {
            var waitTime = Math.abs(ThreadLocalRandom.current().nextInt() % 333L);
            isParked = semParckingSize.tryAcquire(waitTime, TimeUnit.MILLISECONDS);
            if (isParked) {
                logger.debug("[{}] Авто {} припарковался после {}", name, auto, System.currentTimeMillis() - startTime);
                semAutoPlaces.release();
            } else {
                logger.debug("[{}] Авто {} недождался после {} и уехал", name, auto, waitTime);
            }
        } catch (InterruptedException e) {
            logger.error(e.getMessage());
            Thread.currentThread().interrupt();
        }
        return isParked;
    }

    public synchronized boolean get() {
        boolean isFree = false;
        try {
            var waitTime = Math.abs(ThreadLocalRandom.current().nextInt() % 2121L);
            isFree = semAutoPlaces.tryAcquire(waitTime, TimeUnit.MILLISECONDS);
            if (isFree) {
                logger.debug("[{}] Место освобождено", name);
                semParckingSize.release();
            }
        } catch (InterruptedException e) {
            logger.error(e.getMessage());
            Thread.currentThread().interrupt();
        }
        return isFree;
    }
}
