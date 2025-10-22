package com.server.mapper;

import com.common.dto.equipments.EquipmentCreate;
import com.common.dto.equipments.EquipmentRead;
import com.common.dto.equipments.EquipmentUpdate;
import com.server.entity.Equipment;
import com.server.entity.Inventory;
import com.server.entity.Manufacturer;
import org.mapstruct.*;

@Mapper(componentModel = MappingConstants.ComponentModel.SPRING, uses = { MapperUtils.class })
public interface EquipmentMapper {

    EquipmentRead toRead(Equipment entity);

    @Mapping(target = "country", source = "country")
    @Mapping(target = "name", source = "name")
    EquipmentRead.ManufacturerRefInEquipmentRead toRef(Manufacturer entity);

    @Mapping(target = "serialNumber", source = "serialNumber")
    EquipmentRead.InventoryRefInEquipmentRead toRef(Inventory entity);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "inventories", ignore = true)
    @Mapping(target = "type", source = "type.id", qualifiedByName = "typeIdToEquipmentType")
    @Mapping(target = "manufacturer", source = "manufacturer.id", qualifiedByName = "manufacturerIdToManufacturer")
    Equipment toCreate(EquipmentCreate dto);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "inventories", ignore = true)
    @Mapping(target = "type", source = "type.id", qualifiedByName = "typeIdToEquipmentType")
    @Mapping(target = "manufacturer", source = "manufacturer.id", qualifiedByName = "manufacturerIdToManufacturer")
    void update(EquipmentUpdate dto, @MappingTarget Equipment entity);

}
