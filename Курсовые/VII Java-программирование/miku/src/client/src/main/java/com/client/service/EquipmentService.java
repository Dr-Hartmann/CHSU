package com.client.service;

import com.client.base.BaseRestService;
import com.common.dto.equipments.EquipmentCreate;
import com.common.dto.equipments.EquipmentRead;
import com.common.dto.equipments.EquipmentUpdate;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

@Service
public class EquipmentService extends BaseRestService<EquipmentCreate, EquipmentRead, EquipmentUpdate> {

    public EquipmentService(RestClient serverClient) {
        super(serverClient, "equipments", EquipmentRead[].class);
    }

    @Override
    public Object getEntityId(EquipmentRead dto) {
        return dto.id();
    }

}
