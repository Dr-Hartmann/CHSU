package com.server.controller;

import com.common.dto.equipments.EquipmentCreate;
import com.common.dto.equipments.EquipmentRead;
import com.common.dto.equipments.EquipmentUpdate;
import com.server.entity.Equipment;
import com.server.mapper.EquipmentMapper;
import com.server.repository.EquipmentRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.function.BiConsumer;
import java.util.function.Function;

@RestController
@RequestMapping("/api/equipments")
@RequiredArgsConstructor
public class EquipmentController extends BaseController<Equipment, EquipmentRead, EquipmentCreate, EquipmentUpdate> {

    private final EquipmentRepository repository;
    private final EquipmentMapper mapper;

    @Override
    protected JpaRepository<Equipment, Long> repository() {
        return repository;
    }

    @Override
    protected Function<Equipment, EquipmentRead> toRead() {
        return mapper::toRead;
    }

    @Override
    protected Function<EquipmentCreate, Equipment> toCreate() {
        return mapper::toCreate;
    }

    @Override
    protected BiConsumer<EquipmentUpdate, Equipment> update() {
        return mapper::update;
    }

}
