package com.server.controller;

import com.common.dto.inventories.InventoryCreate;
import com.common.dto.inventories.InventoryRead;
import com.common.dto.inventories.InventoryUpdate;
import com.server.entity.Inventory;
import com.server.mapper.InventoryMapper;
import com.server.repository.InventoryRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.function.BiConsumer;
import java.util.function.Function;

@RestController
@RequestMapping("/api/inventories")
@RequiredArgsConstructor
public class InventoryController extends BaseController<Inventory, InventoryRead, InventoryCreate, InventoryUpdate> {

    private final InventoryRepository repository;
    private final InventoryMapper mapper;

    @Override
    protected JpaRepository<Inventory, Long> repository() {
        return repository;
    }

    @Override
    protected Function<Inventory, InventoryRead> toRead() {
        return mapper::toRead;
    }

    @Override
    protected Function<InventoryCreate, Inventory> toCreate() {
        return mapper::toCreate;
    }

    @Override
    protected BiConsumer<InventoryUpdate, Inventory> update() {
        return mapper::update;
    }

}
