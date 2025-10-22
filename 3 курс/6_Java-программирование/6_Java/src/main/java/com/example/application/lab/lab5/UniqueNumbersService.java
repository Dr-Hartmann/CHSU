package com.example.application.lab.lab5;

import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.stream.Collectors;
import org.springframework.stereotype.Service;

@Service
public class UniqueNumbersService {
    public String getNumbersFromOddDigits(InputStream inputStream) {
        Set<Integer> outList = ConcurrentHashMap.newKeySet();

        try (var dis = new DataInputStream(new BufferedInputStream(inputStream))) {
            while (dis.available() > 0) {
                var value = dis.readInt();
                if (oddNumbersCondition(value)) {
                    outList.add(value);
                }
            }
        } catch (IOException e) {
            throw new IllegalArgumentException(e);
        }

        return outList.stream()
                .sorted()
                .map(String::valueOf)
                .collect(Collectors.joining(" "));
    }

    public String getNumbers(InputStream inputStream) {
        var sb = new StringBuilder();

        try (var dis = new DataInputStream(new BufferedInputStream(inputStream))) {
            while (dis.available() > 0) {
                int value = dis.readInt();
                sb.append(value).append(" ");
            }
        } catch (IOException e) {
            throw new IllegalArgumentException(e);
        }

        if (!sb.isEmpty()) {
            sb.setLength(sb.length() - 1);
        }

        return sb.toString();
    }

    private boolean oddNumbersCondition(Integer number) {
        number = Math.abs(number);

        if (number == 0) {
            return false;
        }

        while (number > 0) {
            if ((number % 10) % 2 == 0) {
                return false;
            }
            number /= 10;
        }

        return true;
    }
}
