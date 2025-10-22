package com.server.controller;

import com.common.dto.locations.LocationCreate;
import com.common.dto.locations.LocationRead;
import com.common.dto.locations.LocationUpdate;
import com.server.entity.Location;
import com.server.mapper.LocationMapper;
import com.server.repository.LocationRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.function.BiConsumer;
import java.util.function.Function;

@RestController
@RequestMapping("/api/locations")
@RequiredArgsConstructor
public class LocationController extends BaseController<Location, LocationRead, LocationCreate, LocationUpdate> {

    private final LocationRepository repository;
    private final LocationMapper mapper;

    @Override
    protected JpaRepository<Location, Long> repository() {
        return repository;
    }

    @Override
    protected Function<Location, LocationRead> toRead() {
        return mapper::toRead;
    }

    @Override
    protected Function<LocationCreate, Location> toCreate() {
        return mapper::toCreate;
    }

    @Override
    protected BiConsumer<LocationUpdate, Location> update() {
        return mapper::update;
    }

}
