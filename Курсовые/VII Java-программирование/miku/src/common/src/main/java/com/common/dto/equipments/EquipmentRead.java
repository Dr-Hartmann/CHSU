package com.common.dto.equipments;

import java.time.Instant;
import java.util.Set;

public record EquipmentRead(
        Long id,
        String name,
        EquipmentTypeInRefEquipmentRead type,
        ManufacturerRefInEquipmentRead manufacturer,
        Set<InventoryRefInEquipmentRead> inventories,
        Instant createdDate,
        Instant lastModifiedDate
) {
    public record EquipmentTypeInRefEquipmentRead(
            Long id,
            String name
    ) {
    }

    public record ManufacturerRefInEquipmentRead(
            Long id,
            String name,
            String country
    ) {
    }

    public record InventoryRefInEquipmentRead(
            Long id,
            String serialNumber
    ) {
    }
}
