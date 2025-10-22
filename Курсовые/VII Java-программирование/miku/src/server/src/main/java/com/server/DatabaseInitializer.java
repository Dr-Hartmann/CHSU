package com.server;

import com.common.dto.inventories.InventoryStatus;
import com.server.entity.*;
import com.server.repository.*;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Profile;

import java.util.List;

@Configuration
@Profile(value = {"dev", "prod", "test"})
@RequiredArgsConstructor
public class DatabaseInitializer {

    private final EquipmentRepository equipmentRepository;
    private final EquipmentTypeRepository equipmentTypeRepository;
    private final ManufacturerRepository manufacturerRepository;
    private final LocationRepository locationRepository;
    private final InventoryRepository inventoryRepository;

    @Bean
    CommandLineRunner initDatabase() {
        return _ -> {
            if (equipmentRepository.count() > 0 || equipmentTypeRepository.count() > 0
                    || manufacturerRepository.count() > 0 || locationRepository.count() > 0
                    || inventoryRepository.count() > 0)
                return;

            var router = equipmentTypeRepository.save(EquipmentType.of("Маршрутизатор", null));
            var switchType = equipmentTypeRepository.save(EquipmentType.of("Коммутатор", null));
            var nas = equipmentTypeRepository.save(EquipmentType.of("NAS", null));
            equipmentTypeRepository.save(EquipmentType.of("Кабель", null));
            var accessPoint = equipmentTypeRepository.save(EquipmentType.of("Точка доступа", null));
            var server = equipmentTypeRepository.save(EquipmentType.of("Сервер", null));
            var firewall = equipmentTypeRepository.save(EquipmentType.of("Межсетевой экран", null));
            var ups = equipmentTypeRepository.save(EquipmentType.of("ИБП", null));
            equipmentTypeRepository.save(EquipmentType.of("СХД", null));
            equipmentTypeRepository.save(EquipmentType.of("KVM-переключатель", null));

            var cisco = manufacturerRepository.save(Manufacturer.of("Cisco", "США", null));
            var tai = "Тайвань";
            manufacturerRepository.save(Manufacturer.of("D-Link", tai, null));
            manufacturerRepository.save(Manufacturer.of("Huawei", "Китай", null));
            var mikrotik = manufacturerRepository.save(Manufacturer.of("MikroTik", "Латвия", null));
            var hp = manufacturerRepository.save(Manufacturer.of("HP Enterprise", "США", null));
            var dell = manufacturerRepository.save(Manufacturer.of("Dell", "США", null));
            var juniper = manufacturerRepository.save(Manufacturer.of("Juniper", "США", null));
            var apc = manufacturerRepository.save(Manufacturer.of("APC", "США", null));
            manufacturerRepository.save(Manufacturer.of("Zyxel", tai, null));
            var qnap = manufacturerRepository.save(Manufacturer.of("QNAP", tai, null));

            var r1 = equipmentRepository.save(Equipment.of("Cisco 2911/K9", router, cisco, null));
            equipmentRepository.save(Equipment.of("D-Link DGS-1210-27", switchType, apc, null));
            var s1 = equipmentRepository.save(Equipment.of("D-Link DGS-1210-28", switchType, apc, null));
            var nas1 = equipmentRepository.save(Equipment.of("QNAP TS-453D", nas, qnap, null));
            var fw1 = equipmentRepository.save(Equipment.of("Firepower 1010", firewall, cisco, null));
            equipmentRepository.save(Equipment.of("hAP ac7", accessPoint, apc, null));
            var ap1 = equipmentRepository.save(Equipment.of("hAP ac2", accessPoint, mikrotik, null));
            var dl380 = equipmentRepository.save(Equipment.of("ProLiant DL380 Gen10", server, hp, null));
            var r740 = equipmentRepository.save(Equipment.of("PowerEdge R740", server, dell, null));
            equipmentRepository.save(Equipment.of("Juniper MX207", router, apc, null));
            var mx204 = equipmentRepository.save(Equipment.of("Juniper MX204", router, juniper, null));
            var ups1500 = equipmentRepository.save(Equipment.of("Smart-UPS 1500VA", ups, apc, null));
            var n9k = equipmentRepository.save(Equipment.of("Nexus 9300", switchType, cisco, null));

            var loc1 = locationRepository.save(Location.of("Москва, ДЦ-1, зал A", "RACK-A-01", null));
            var loc2 = locationRepository.save(Location.of("Москва, ДЦ-1, зал B", "RACK-B-03", null));
            var loc3 = locationRepository.save(Location.of("СПб, ДЦ-2, зал C", "RACK-C-07", null));
            var loc4 = locationRepository.save(Location.of("СПб, ДЦ-2, зал D", "RACK-D-12", null));
            var loc5 = locationRepository.save(Location.of("Екатеринбург, Офис", "Srv-Room-1", null));
            var loc6 = locationRepository.save(Location.of("Новосибирск, ДЦ-3", "RACK-01", null));
            var loc7 = locationRepository.save(Location.of("Казань, Склад", "Shelf-04", null));
            locationRepository.save(Location.of("Краснодар, Офис", "Cabinet-2", null));
            locationRepository.save(Location.of("Нижний Новгород, ТО", "Room-10", null));
            locationRepository.save(Location.of("Владивосток, ДЦ-4", "RACK-F-02", null));

            inventoryRepository.saveAll(List.of(
                    Inventory.of(r1, "SN-CISCO-001", loc1, InventoryStatus.ACTIVE),
                    Inventory.of(s1, "SN-DLINK-102", loc3, InventoryStatus.RESERVED),
                    Inventory.of(nas1, "SN-QNAP-990", loc2, InventoryStatus.STOCK),
                    Inventory.of(fw1, "SN-ASA-5506", loc1, InventoryStatus.REPAIR),
                    Inventory.of(ap1, "SN-MIKRO-777", loc5, InventoryStatus.TESTING),
                    Inventory.of(dl380, "SN-HP-DL380-G10-1", loc2, InventoryStatus.BROKEN),
                    Inventory.of(dl380, "SN-HP-DL380-G10-2", loc2, InventoryStatus.STOCK),
                    Inventory.of(r740, "SN-DELL-R740-01", loc4, InventoryStatus.LOST),
                    Inventory.of(mx204, "SN-JUN-MX204", loc6, InventoryStatus.ACTIVE),
                    Inventory.of(ups1500, "SN-APC-1500-01", loc1, InventoryStatus.ACTIVE),
                    Inventory.of(n9k, "SN-NEXUS-9000", loc4, InventoryStatus.WARRANTY_REPLACEMENT),
                    Inventory.of(r1, "SN-CISCO-OLD-01", loc7, InventoryStatus.DECOMMISSIONED)
            ));
        };
    }
}
