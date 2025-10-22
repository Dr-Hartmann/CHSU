package com.server.mapper;

import com.common.dto.inventories.InventoryStatus;
import com.server.entity.Equipment;
import com.server.entity.EquipmentType;
import com.server.entity.Location;
import com.server.entity.Manufacturer;
import com.server.repository.EquipmentRepository;
import com.server.repository.EquipmentTypeRepository;
import com.server.repository.LocationRepository;
import com.server.repository.ManufacturerRepository;
import lombok.RequiredArgsConstructor;
import org.mapstruct.Named;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Component;

@Component
@Named("MapperUtils")
@RequiredArgsConstructor
public class MapperUtils {

//    @Named("getHashFromName")
//    public String getHashFromName(String name) {
//        return name + Integer.toHexString(name.hashCode());
//    }

    private final EquipmentTypeRepository equipmentTypeRepository;

    @Named("typeIdToEquipmentType")
    public EquipmentType typeIdToEquipmentType(Long id) {
        return findByIdOrThrow(equipmentTypeRepository, id, "Тип оборудования не найден.");
    }

    private final ManufacturerRepository manufacturerRepository;

    @Named("manufacturerIdToManufacturer")
    public Manufacturer manufacturerIdToManufacturer(Long id) {
        return findByIdOrThrow(manufacturerRepository, id, "Производитель не найден.");
    }

    private final LocationRepository locationRepository;

    @Named("locationIdToLocation")
    public Location locationIdToLocation(Long id) {
        return findByIdOrThrow(locationRepository, id, "Локация '" + id + "' не найдена.");
    }

    private final EquipmentRepository equipmentRepository;

    @Named("equipmentIdToEquipment")
    public Equipment equipmentIdToEquipment(Long id) {
        return findByIdOrThrow(equipmentRepository, id, "Устройство '" + id + "' не найдено.");
    }

    private <T, I> T findByIdOrThrow(JpaRepository<T, I> repository, I id, String error) {
        return id == null ? null : repository.findById(id).orElseThrow(() -> new IllegalArgumentException(error));
    }

    @Named("mapStatusDescription")
    public String mapStatusDescription(InventoryStatus status) {
        return (status != null) ? status.getDescription() : null;
    }

//    private <T> Set<Long> entitiesToIds(Set<T> items, Function<T, Long> idMapper) {
//        return items == null ? Collections.emptySet() : items.stream().map(idMapper).collect(Collectors.toSet());
//    }
//
//    private <T, I> Set<T> idsToEntities(Set<I> ids, JpaRepository<T, I> repository, Function<I, String> error) {
//        return ids == null ? Collections.emptySet()
//                : ids.stream().map(
//                        id -> repository.findById(id).orElseThrow(() -> new IllegalArgumentException(error.apply(id))))
//                .collect(Collectors.toSet());
//    }

}
