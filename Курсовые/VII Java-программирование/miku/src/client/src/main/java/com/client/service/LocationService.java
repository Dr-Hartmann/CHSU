package com.client.service;

import com.client.base.BaseRestService;
import com.common.dto.locations.LocationCreate;
import com.common.dto.locations.LocationRead;
import com.common.dto.locations.LocationUpdate;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestClient;

@Service
public class LocationService extends BaseRestService<LocationCreate, LocationRead, LocationUpdate> {

    public LocationService(RestClient serverClient) {
        super(serverClient, "locations", LocationRead[].class);
    }

    @Override
    public Object getEntityId(LocationRead dto) {
        return dto.id();
    }

}
