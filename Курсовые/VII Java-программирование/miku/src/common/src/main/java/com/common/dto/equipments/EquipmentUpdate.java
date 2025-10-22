package com.common.dto.equipments;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;

public record EquipmentUpdate(
        @NotNull(message = "ID оборудования обязателен для обновления")
        Long id,

        @NotBlank(message = "Имя оборудования обязательно")
        @Size(max = 100, message = "Имя не может превышать 100 символов")
        @Pattern(
                regexp = "^[A-ZА-ЯЁ0-9][a-zA-Zа-яА-ЯЁёЫыЪъЬь0-9\\s\\-]*$",
                message = "Имя должно начинаться с заглавной буквы или цифры и содержать только допустимые символы"
        )
        String name,

        @NotNull(message = "Тип оборудования обязателен")
        @Valid
        EquipmentTypeRefInEquipmentUpdate type,

        @NotNull(message = "Производитель обязателен")
        @Valid
        ManufacturerRefInEquipmentUpdate manufacturer
) {
    public record EquipmentTypeRefInEquipmentUpdate(
            @NotNull Long id
    ) {
    }

    public record ManufacturerRefInEquipmentUpdate(
            @NotNull Long id
    ) {
    }
}
