package com.server.entity;

import com.common.dto.inventories.InventoryStatus;
import jakarta.persistence.*;
import lombok.*;

import java.time.Instant;

@Entity
@Table(name = "inventories")
@Getter
@Setter
@NoArgsConstructor
@EqualsAndHashCode(callSuper = false, exclude = {"location", "equipment"})
public class Inventory extends BaseEntity {

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "model_id", nullable = false)
    private Equipment equipment;

    @Column(name = "serial_number", length = 100, nullable = false, unique = true)
    private String serialNumber;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "location_id")
    private Location location;

    @Column(length = 50)
    @Enumerated(EnumType.STRING)
    private InventoryStatus status = InventoryStatus.ACTIVE;

//    @ManyToMany
//    @JoinTable(name = "user_posts", joinColumns = @JoinColumn(name = "user_id"), inverseJoinColumns = @JoinColumn(name = "post_id"))
//    private Set<Post> posts = new LinkedHashSet<>();

    @Builder
    private Inventory(Equipment equipment, String serialNumber, Location location, InventoryStatus status, Instant createdDate, Instant lastModifiedDate) {
        super(createdDate, lastModifiedDate);

        if (equipment == null)
            throw new IllegalArgumentException("Оборудование обязательно для передачи");
        if (serialNumber == null || serialNumber.isBlank())
            throw new IllegalArgumentException("Серийный номер оборудования обязательно");

        this.equipment = equipment;
        this.serialNumber = serialNumber;
        this.location = location;
        this.status = status;
    }

    public static Inventory of(Equipment equipment, String serialNumber, Location location, InventoryStatus status) {
        return Inventory.builder().equipment(equipment).serialNumber(serialNumber).location(location).status(status).build();
    }

}
