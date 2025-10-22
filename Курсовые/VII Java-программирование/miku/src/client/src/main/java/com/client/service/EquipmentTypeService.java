package com.client.service;

import com.client.base.BaseRestService;
import com.common.dto.equipment_types.EquipmentTypeCreate;
import com.common.dto.equipment_types.EquipmentTypeRead;
import com.common.dto.equipment_types.EquipmentTypeUpdate;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

@Service
public class EquipmentTypeService extends BaseRestService<EquipmentTypeCreate, EquipmentTypeRead, EquipmentTypeUpdate> {

    public EquipmentTypeService(RestClient serverClient) {
        super(serverClient, "equipments-types", EquipmentTypeRead[].class);
    }

    @Override
    public Object getEntityId(EquipmentTypeRead dto) {
        return dto.id();
    }
    
}
