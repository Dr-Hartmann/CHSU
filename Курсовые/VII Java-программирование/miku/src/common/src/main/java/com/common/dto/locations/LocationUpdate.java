package com.common.dto.locations;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;

public record LocationUpdate(
        @NotNull(message = "ID расположения обязателен для обновления")
        Long id,

        @NotBlank(message = "Адрес расположения обязателен")
        @Size(max = 100, message = "Адрес не может превышать 100 символов")
        @Pattern(
                regexp = "^[A-ZА-ЯЁ0-9].*$",
                message = "Адрес должен начинаться с заглавной буквы или цифры"
        )
        String address,

        @NotBlank(message = "Регистрационный номер (стойка) обязателен")
        @Size(max = 100, message = "Номер стойки не может превышать 100 символов")
        @Pattern(
                regexp = "^[A-Z0-9]{2,5}-\\d{2,3}-[A-Z]$",
                message = "Формат номера стойки: ЗДАНИЕ(2-5 символов)-ЭТАЖ(2-3 цифры)-СЕКЦИЯ(1 символ). Пример: DC01-04-A"
        )
        String rackNumber
) {
}
