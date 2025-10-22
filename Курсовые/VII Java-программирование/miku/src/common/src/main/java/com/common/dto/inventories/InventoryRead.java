package com.common.dto.inventories;

import java.time.Instant;

public record InventoryRead(
        Long id,
        String serialNumber,
        InventoryStatus status,
        String statusDescription,
        EquipmentRefInInventoryRead equipment,
        LocationRefInInventoryRead location,
        Instant createdDate,
        Instant lastModifiedDate
) {
    public record EquipmentRefInInventoryRead(
            Long id,
            String name,
            String typeName,
            String manufacturerName
    ) {
    }

    public record LocationRefInInventoryRead(
            Long id,
            String address,
            String rackNumber
    ) {
    }
}
