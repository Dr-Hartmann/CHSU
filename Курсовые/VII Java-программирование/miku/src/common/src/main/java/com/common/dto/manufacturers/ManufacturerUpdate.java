package com.common.dto.manufacturers;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;

public record ManufacturerUpdate(
        @NotNull(message = "ID производителя обязателен для обновления")
        Long id,

        @NotBlank(message = "Имя производителя обязательно")
        @Size(max = 100, message = "Имя не может превышать 100 символов")
        @Pattern(
                regexp = "^[A-ZА-ЯЁ0-9].*$",
                message = "Имя производителя должно начинаться с заглавной буквы или цифры"
        )
        String name,

        @NotBlank(message = "Страна производства обязательна")
        @Size(max = 100, message = "Название страны не может превышать 100 символов")
        @Pattern(
                regexp = "^[A-ZА-ЯЁЫ][a-zа-яA-ZА-ЯЫыЁёЪъЬь\\s\\-]*$",
                message = "Название страны должно начинаться с заглавной буквы и содержать только буквы"
        )
        String country
) {
}
