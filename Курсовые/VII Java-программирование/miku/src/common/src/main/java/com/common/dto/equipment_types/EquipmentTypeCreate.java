package com.common.dto.equipment_types;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;

public record EquipmentTypeCreate(
        @NotBlank(message = "Название типа обязательно")
        @Size(max = 100, message = "Название не может быть длиннее 100 символов")
        @Pattern(
                regexp = "^[A-ZА-ЯЁ0-9][a-zA-Zа-яА-ЯЁёЫыЪъЬь0-9\\s\\-]*$",
                message = "Имя должно начинаться с заглавной буквы или цифры и содержать только допустимые символы"
        )
        String name
) {
}
