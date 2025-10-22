package com.common.dto.inventories;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;

public record InventoryUpdate(
        @NotNull(message = "ID оборудования обязателен для обновления")
        Long id,

        @NotBlank(message = "Серийный номер обязателен")
        @Size(max = 100, message = "Серийный номер не может превышать 100 символов")
        @Pattern(
                regexp = "^[A-Za-z0-9-_./]+$",
                message = "Серийный номер может содержать только латиницу, цифры, дефис, подчеркивание, точку и косую черту"
        )
        String serialNumber,

        @NotNull(message = "Объект оборудования обязателен")
        @Valid
        EquipmentRefInInventoryUpdate equipment,

        @Valid
        LocationRefInInventoryUpdate location,

        @NotNull(message = "Статус обязателен")
        InventoryStatus status
) {
    public record EquipmentRefInInventoryUpdate(
            @NotNull Long id
    ) {
    }

    public record LocationRefInInventoryUpdate(
            @NotNull Long id
    ) {
    }
}
