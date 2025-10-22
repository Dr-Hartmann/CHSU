package com.common.dto.equipment_types;

import java.time.Instant;
import java.util.Set;

public record EquipmentTypeRead(
        Long id,
        String name,
        Set<EquipmentRefInTypeRead> equipments,
        Instant createdDate,
        Instant lastModifiedDate
) {
    public record EquipmentRefInTypeRead(
            Long id,
            String name,
            String manufacturerName
    ) {
    }
}