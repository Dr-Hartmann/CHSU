package com.server.entity;

import jakarta.persistence.*;
import lombok.*;

import java.time.Instant;
import java.util.LinkedHashSet;
import java.util.Set;

@Entity
@Table(name = "equipment_types")
@Getter
@Setter
@NoArgsConstructor
@EqualsAndHashCode(callSuper = false, exclude = {"equipments"})
public class EquipmentType extends BaseEntity {

    @Column(length = 100, nullable = false, unique = true)
    private String name;

    @OneToMany(mappedBy = "type", fetch = FetchType.LAZY)
    private Set<Equipment> equipments = new LinkedHashSet<>();

    @Builder
    private EquipmentType(String name, Set<Equipment> equipments, Instant createdDate, Instant lastModifiedDate) {
        super(createdDate, lastModifiedDate);

        if (name == null || name.isBlank())
            throw new IllegalArgumentException("Имя типа обязательно");

        this.name = name;
        this.equipments = equipments != null ? equipments : new LinkedHashSet<>();
    }

    public static EquipmentType of(String name, Set<Equipment> equipments) {
        return EquipmentType.builder().name(name).equipments(equipments).build();
    }

}
