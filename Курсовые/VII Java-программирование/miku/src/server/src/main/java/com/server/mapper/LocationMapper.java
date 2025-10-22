package com.server.mapper;

import com.common.dto.locations.LocationCreate;
import com.common.dto.locations.LocationRead;
import com.common.dto.locations.LocationUpdate;
import com.server.entity.Inventory;
import com.server.entity.Location;
import org.mapstruct.*;

@Mapper(componentModel = MappingConstants.ComponentModel.SPRING)
public interface LocationMapper {

    LocationRead toRead(Location item);

    @Mapping(target = "equipmentName", source = "equipment.name")
    LocationRead.InventoryRefInLocationRead toRef(Inventory item);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "inventories", ignore = true)
    Location toCreate(LocationCreate dto);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "inventories", ignore = true)
    void update(LocationUpdate dto, @MappingTarget Location entity);

}
