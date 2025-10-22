package com.common.dto.locations;

import java.time.Instant;
import java.util.Set;

public record LocationRead(
        Long id,
        String address,
        String rackNumber,
        Set<InventoryRefInLocationRead> inventories,
        Instant createdDate,
        Instant lastModifiedDate
) {
    public record InventoryRefInLocationRead(
            Long id,
            String serialNumber,
            String equipmentName,
            String status
    ) {
    }
}
