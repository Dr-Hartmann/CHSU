package com.server.controller;

import com.common.dto.manufacturers.ManufacturerCreate;
import com.common.dto.manufacturers.ManufacturerRead;
import com.common.dto.manufacturers.ManufacturerUpdate;
import com.server.entity.Manufacturer;
import com.server.mapper.ManufacturerMapper;
import com.server.repository.ManufacturerRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.function.BiConsumer;
import java.util.function.Function;

@RestController
@RequestMapping("/api/manufacturers")
@RequiredArgsConstructor
public class ManufacturerController extends BaseController<Manufacturer, ManufacturerRead, ManufacturerCreate, ManufacturerUpdate> {

    private final ManufacturerRepository repository;
    private final ManufacturerMapper mapper;

    @Override
    protected JpaRepository<Manufacturer, Long> repository() {
        return repository;
    }

    @Override
    protected Function<Manufacturer, ManufacturerRead> toRead() {
        return mapper::toRead;
    }

    @Override
    protected Function<ManufacturerCreate, Manufacturer> toCreate() {
        return mapper::toCreate;
    }

    @Override
    protected BiConsumer<ManufacturerUpdate, Manufacturer> update() {
        return mapper::update;
    }

}
