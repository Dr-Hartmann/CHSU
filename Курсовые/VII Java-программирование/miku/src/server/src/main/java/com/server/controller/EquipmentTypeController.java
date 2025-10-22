package com.server.controller;

import com.common.dto.equipment_types.EquipmentTypeCreate;
import com.common.dto.equipment_types.EquipmentTypeRead;
import com.common.dto.equipment_types.EquipmentTypeUpdate;
import com.server.entity.EquipmentType;
import com.server.mapper.EquipmentTypeMapper;
import com.server.repository.EquipmentTypeRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.function.BiConsumer;
import java.util.function.Function;

@RestController
@RequestMapping("/api/equipments-types")
@RequiredArgsConstructor
public class EquipmentTypeController extends BaseController<EquipmentType, EquipmentTypeRead, EquipmentTypeCreate, EquipmentTypeUpdate> {

    private final EquipmentTypeRepository repository;
    private final EquipmentTypeMapper mapper;

    @Override
    protected JpaRepository<EquipmentType, Long> repository() {
        return repository;
    }

    @Override
    protected Function<EquipmentType, EquipmentTypeRead> toRead() {
        return mapper::toRead;
    }

    @Override
    protected Function<EquipmentTypeCreate, EquipmentType> toCreate() {
        return mapper::toCreate;
    }

    @Override
    protected BiConsumer<EquipmentTypeUpdate, EquipmentType> update() {
        return mapper::update;
    }

}
