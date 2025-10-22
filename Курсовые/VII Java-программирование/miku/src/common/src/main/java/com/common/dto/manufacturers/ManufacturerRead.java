package com.common.dto.manufacturers;

import java.time.Instant;
import java.util.Set;

public record ManufacturerRead(
        Long id,
        String name,
        String country,
        Set<EquipmentRefInManufacturerRead> equipments,
        Instant createdDate,
        Instant lastModifiedDate
) {
    public record EquipmentRefInManufacturerRead(
            Long id,
            String name,
            String typeName
    ) {
    }
}
