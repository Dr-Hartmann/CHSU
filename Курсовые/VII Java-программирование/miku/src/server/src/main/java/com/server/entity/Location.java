package com.server.entity;

import jakarta.persistence.*;
import lombok.*;

import java.time.Instant;
import java.util.LinkedHashSet;
import java.util.Set;

@Entity
@Table(name = "locations")
@Getter
@Setter
@NoArgsConstructor
@EqualsAndHashCode(callSuper = false, exclude = {"inventories"})
public class Location extends BaseEntity {

    @Column(length = 100, nullable = false)
    private String address;

    @Column(name = "rack_number", length = 100, unique = true)
    private String rackNumber;

    @OneToMany(mappedBy = "location", fetch = FetchType.LAZY)
    private Set<Inventory> inventories = new LinkedHashSet<>();

    @Builder
    private Location(String address, String rackNumber, Set<Inventory> inventories, Instant createdDate,
            Instant lastModifiedDate) {
        super(createdDate, lastModifiedDate);

        if (address == null || address.isBlank())
            throw new IllegalArgumentException("Адрес расположения обязателен");
        if (rackNumber == null || rackNumber.isBlank())
            throw new IllegalArgumentException("Регистрационный номер обязателен");

        this.address = address;
        this.rackNumber = rackNumber;
        this.inventories = inventories != null ? inventories : new LinkedHashSet<>();
    }

    public static Location of(String address, String rackNumber, Set<Inventory> inventories) {
        return Location.builder().address(address).rackNumber(rackNumber).inventories(inventories).build();
    }

}
