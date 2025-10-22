package com.common.dto.inventories;

public enum InventoryStatus {
    ACTIVE("В эксплуатации"),
    REPAIR("На ремонте"),
    STOCK("На складе"),
    DECOMMISSIONED("Списан"),
    TRANSIT("В пути (логистика)"),
    TESTING("На тестировании"),
    RESERVED("Зарезервировано"),
    BROKEN("Неисправен (ожидает решения)"),
    LOST("Утерян/Кража"),
    WARRANTY_REPLACEMENT("Гарантийная замена");

    private final String description;

    InventoryStatus(String description) {
        this.description = description;
    }

    public String getDescription() {
        return description;
    }
}
