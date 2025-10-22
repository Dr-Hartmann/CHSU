package com.common.dto.equipments;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;

public record EquipmentCreate(
        @NotBlank(message = "Имя оборудования обязательно")
        @Size(max = 100, message = "Имя не может превышать 100 символов")
        @Pattern(
                regexp = "^[A-ZА-ЯЁ0-9][a-zA-Zа-яА-ЯЁёЫыЪъЬь0-9\\s\\-]*$",
                message = "Имя должно начинаться с заглавной буквы или цифры и содержать только допустимые символы"
        )
        String name,

        @NotNull(message = "Тип оборудования обязателен")
        @Valid
        EquipmentTypeInRefInEquipmentCreate type,

        @NotNull(message = "Производитель обязателен")
        @Valid
        ManufacturerRefInEquipmentCreate manufacturer
) {
    public record EquipmentTypeInRefInEquipmentCreate(
            @NotNull Long id
    ) {
    }

    public record ManufacturerRefInEquipmentCreate(
            @NotNull Long id
    ) {
    }
}
