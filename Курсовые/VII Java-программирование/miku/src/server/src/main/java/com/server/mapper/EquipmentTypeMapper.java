package com.server.mapper;

import com.common.dto.equipment_types.EquipmentTypeCreate;
import com.common.dto.equipment_types.EquipmentTypeRead;
import com.common.dto.equipment_types.EquipmentTypeUpdate;
import com.server.entity.Equipment;
import com.server.entity.EquipmentType;
import org.mapstruct.*;

@Mapper(componentModel = MappingConstants.ComponentModel.SPRING)
public interface EquipmentTypeMapper {

    EquipmentTypeRead toRead(EquipmentType entity);

    @Mapping(target = "manufacturerName", source = "manufacturer.name")
    EquipmentTypeRead.EquipmentRefInTypeRead toRef(Equipment entity);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "equipments", ignore = true)
    EquipmentType toCreate(EquipmentTypeCreate dto);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "equipments", ignore = true)
    void update(EquipmentTypeUpdate dto, @MappingTarget EquipmentType entity);

}
