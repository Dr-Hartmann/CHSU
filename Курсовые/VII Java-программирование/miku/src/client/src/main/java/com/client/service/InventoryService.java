package com.client.service;

import com.client.base.BaseRestService;
import com.common.dto.inventories.InventoryCreate;
import com.common.dto.inventories.InventoryRead;
import com.common.dto.inventories.InventoryUpdate;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

@Service
public class InventoryService extends BaseRestService<InventoryCreate, InventoryRead, InventoryUpdate> {

    public InventoryService(RestClient serverClient) {
        super(serverClient, "inventories", InventoryRead[].class);
    }

    @Override
    public Object getEntityId(InventoryRead dto) {
        return dto.id();
    }

}
