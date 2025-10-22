package com.server.mapper;

import com.common.dto.inventories.InventoryCreate;
import com.common.dto.inventories.InventoryRead;
import com.common.dto.inventories.InventoryUpdate;
import com.server.entity.Equipment;
import com.server.entity.Inventory;

import org.mapstruct.*;

@Mapper(componentModel = MappingConstants.ComponentModel.SPRING, uses = { MapperUtils.class })
public interface InventoryMapper {

    @Mapping(target = "statusDescription", source = "status", qualifiedByName = "mapStatusDescription")
    InventoryRead toRead(Inventory entity);

    @Mapping(target = "typeName", source = "type.name")
    @Mapping(target = "manufacturerName", source = "manufacturer.name")
    InventoryRead.EquipmentRefInInventoryRead toRef(Equipment entity);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "equipment", source = "equipment.id", qualifiedByName = "equipmentIdToEquipment")
    @Mapping(target = "location", source = "location.id", qualifiedByName = "locationIdToLocation")
    Inventory toCreate(InventoryCreate dto);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "equipment", source = "equipment.id", qualifiedByName = "equipmentIdToEquipment")
    @Mapping(target = "location", source = "location.id", qualifiedByName = "locationIdToLocation")
    void update(InventoryUpdate dto, @MappingTarget Inventory entity);

}
