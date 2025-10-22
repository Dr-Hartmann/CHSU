package com.server.mapper;

import com.common.dto.manufacturers.ManufacturerCreate;
import com.common.dto.manufacturers.ManufacturerRead;
import com.common.dto.manufacturers.ManufacturerUpdate;
import com.server.entity.Equipment;
import com.server.entity.Manufacturer;
import org.mapstruct.*;

@Mapper(componentModel = MappingConstants.ComponentModel.SPRING)
public interface ManufacturerMapper {

    ManufacturerRead toRead(Manufacturer entity);

    @Mapping(target = "typeName", source = "type.name")
    ManufacturerRead.EquipmentRefInManufacturerRead toRef(Equipment entity);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "equipments", ignore = true)
    Manufacturer toCreate(ManufacturerCreate dto);

    @Mapping(target = "createdDate", ignore = true)
    @Mapping(target = "lastModifiedDate", ignore = true)
    @Mapping(target = "equipments", ignore = true)
    void update(ManufacturerUpdate dto, @MappingTarget Manufacturer entity);

}
