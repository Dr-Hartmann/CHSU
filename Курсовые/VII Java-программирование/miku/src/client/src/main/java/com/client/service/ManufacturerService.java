package com.client.service;

import com.client.base.BaseRestService;
import com.common.dto.manufacturers.ManufacturerCreate;
import com.common.dto.manufacturers.ManufacturerRead;

import com.common.dto.manufacturers.ManufacturerUpdate;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

@Service
public class ManufacturerService extends BaseRestService<ManufacturerCreate, ManufacturerRead, ManufacturerUpdate> {

    public ManufacturerService(RestClient serverClient) {
        super(serverClient, "manufacturers", ManufacturerRead[].class);
    }

    @Override
    public Object getEntityId(ManufacturerRead dto) {
        return dto.id();
    }

}
