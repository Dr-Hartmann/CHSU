package com.example;

import java.io.Console;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Properties;

import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import com.example.lab1.WindowUpdater;
import com.example.lab2.Parking;
import com.example.lab2.ParkingFill;
import com.example.lab2.ParkingFree;
import com.example.lab3.Lab3;

import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Slf4j
@Component
@RequiredArgsConstructor
public class ConsoleRunner implements CommandLineRunner {
    private final Lab3 lab3;

    @Override
    public void run(String... args) {
        var console = System.console();
        if (console == null) {
            log.error("Консоль недоступна");
            return;
        }

        while (true) {
            int num = readTaskNumber(console);

            if (num == 0) {
                log.info("Выход из программы.");
                break;
            }

            switch (num) {
                case 1 -> lab1();
                case 2 -> lab2();
                case 3 -> lab3.prepareTable("3.xls");
                default -> log.warn("Задание не существует");
            }
        }
    }

    private int readTaskNumber(Console console) {
        int num = -1;
        while (num <= 0) {
            try {
                num = Integer.parseInt(console.readLine("Введите номер задания: "));
            } catch (NumberFormatException e) {
                log.warn("Ввод не является числом");
            }
        }
        return num;
    }

    private void lab1() {
        WindowUpdater.case1();
        WindowUpdater.case2();
    }

    private void lab2() {
        var props = new Properties();
        var areas = new ArrayList<Parking>();

        try (var is = Main.class.getClassLoader().getResourceAsStream("lab2.properties")) {
            props.load(new InputStreamReader(is, StandardCharsets.UTF_8));
            props.forEach((k, v) -> areas.add(new Parking(k.toString(), Integer.parseInt(v.toString()))));

            new ParkingFill(areas);
            // new ParkingFill(areas);
            new ParkingFree(areas);
            // new ParkingFree(areas);
        } catch (IOException e) {
            log.error(e.toString());
        }
    }
}
