package com.server.entity;

import jakarta.persistence.*;
import lombok.*;

import java.time.Instant;
import java.util.LinkedHashSet;
import java.util.Set;

@Entity
@Table(name = "manufacturers")
@Getter
@Setter
@NoArgsConstructor
@EqualsAndHashCode(callSuper = false, exclude = {"equipments"})
public class Manufacturer extends BaseEntity {

    @Column(length = 100, nullable = false, unique = true)
    private String name;

    @Column(length = 100)
    private String country;

    @OneToMany(mappedBy = "manufacturer", fetch = FetchType.LAZY)
    private Set<Equipment> equipments = new LinkedHashSet<>();

    @Builder
    private Manufacturer(String name, String country, Set<Equipment> equipments, Instant createdDate,
            Instant lastModifiedDate) {
        super(createdDate, lastModifiedDate);

        if (name == null || name.isBlank())
            throw new IllegalArgumentException("Имя производителя обязательно");
        if (country == null || country.isBlank())
            throw new IllegalArgumentException("Страна производителя обязательна");

        this.name = name;
        this.country = country;
        this.equipments = equipments != null ? equipments : new LinkedHashSet<>();
    }

    public static Manufacturer of(String name, String country, Set<Equipment> equipment) {
        return Manufacturer.builder().name(name).country(country).equipments(equipment).build();
    }

}
