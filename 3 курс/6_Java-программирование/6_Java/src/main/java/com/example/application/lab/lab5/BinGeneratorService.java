package com.example.application.lab.lab5;

import java.io.DataOutputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.concurrent.ThreadLocalRandom;
import org.springframework.stereotype.Service;
import lombok.NoArgsConstructor;

@Service
@NoArgsConstructor
public class BinGeneratorService {
    public String generateFileAsync(String filename, Integer size, Integer max, Integer min) {
        if (filename == null || size == null || max == null || min == null || filename.isBlank() || size < 0) {
            throw new IllegalArgumentException("Некорректные параметры");
        }

        if (max < min) {
            var tmp = max;
            max = min;
            min = tmp;
        }

        var fullname = filename + ".bin";
        try (var dos = new DataOutputStream(new FileOutputStream(fullname))) {
            for (int i = 0; i < size; i++) {
                var c = ThreadLocalRandom.current().nextInt(min, max);
                dos.writeInt(c);
            }
        } catch (IOException e) {
            throw new IllegalArgumentException(e);
        }

        return fullname;
    }
}
