package com.server.entity;

import jakarta.persistence.*;
import lombok.*;

import java.time.Instant;
import java.util.LinkedHashSet;
import java.util.Set;

@Entity
@Table(name = "equipments")
@Getter
@Setter
@NoArgsConstructor
@EqualsAndHashCode(callSuper = false, exclude = {"type", "manufacturer", "inventories"})
public class Equipment extends BaseEntity {

    @Column(length = 100, nullable = false, unique = true)
    private String name;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "type_id")
    private EquipmentType type;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "manufacturer_id")
    private Manufacturer manufacturer;

    @OneToMany(mappedBy = "equipment", fetch = FetchType.LAZY)
    private Set<Inventory> inventories = new LinkedHashSet<>();

    @Builder
    private Equipment(String name, EquipmentType type, Manufacturer manufacturer, Set<Inventory> inventories,
            Instant createdDate, Instant lastModifiedDate) {
        super(createdDate, lastModifiedDate);

        if (name == null || name.isBlank())
            throw new IllegalArgumentException("Имя оборудования обязательно");

        this.name = name;
        this.type = type;
        this.manufacturer = manufacturer;
        this.inventories = inventories != null ? inventories : new LinkedHashSet<>();
    }

    public static Equipment of(String name, EquipmentType type, Manufacturer manufacturer, Set<Inventory> inventories) {
        return Equipment.builder().name(name).type(type).manufacturer(manufacturer).inventories(inventories).build();
    }

}
