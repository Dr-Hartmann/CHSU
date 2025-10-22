package com.client.base;

import com.vaadin.flow.data.provider.ListDataProvider;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;
import org.springframework.web.client.ResourceAccessException;
import org.springframework.web.client.RestClient;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

@Service
@Slf4j
@RequiredArgsConstructor
public abstract class BaseRestService<C, R, U> {

    protected abstract Object getEntityId(R dto);

    public long getDataSize() {
        if (dataProvider.getItems().isEmpty()) {
            refresh();
        }
        return dataProvider.getItems().size();
    }

    public List<R> getDataList() {
        if (dataProvider.getItems().isEmpty()) {
            refresh();
        }
        return dataProvider.getItems().stream().toList();
    }

    public List<R> readAll() {
        try {
            var response = serverClient.get()
                    .uri(API_PATH + tableName)
                    .retrieve()
                    .body(readArrayClass);
            return response != null ? new ArrayList<>(Arrays.asList(response)) : new ArrayList<>();
        } catch (ResourceAccessException e) {
            log.error(e.getMessage());
        }
        return Collections.emptyList();
    }

    public void create(C dto) {
        try {
            serverClient.post()
                    .uri(API_PATH + tableName)
                    .body(dto)
                    .retrieve()
                    .toBodilessEntity();
            refresh();
        } catch (ResourceAccessException e) {
            log.error(e.getMessage());
        }
    }

    public void delete(Object id) {
        try {
            serverClient.delete()
                    .uri(API_PATH + tableName + "/{id}", id)
                    .retrieve()
                    .toBodilessEntity();
            refresh();
        } catch (ResourceAccessException e) {
            log.error(e.getMessage());
        }
    }

    public void update(Object id, U dto) {
        try {
            serverClient.put()
                    .uri(API_PATH + tableName + "/{id}", id)
                    .body(dto)
                    .retrieve()
                    .toBodilessEntity();

            refresh();
        } catch (ResourceAccessException e) {
            log.error(e.getMessage());
        }
    }

    private void refresh() {
        var data = readAll();
        dataProvider.getItems().clear();
        if (data != null) {
            dataProvider.getItems().addAll(data);
        }
        dataProvider.refreshAll();
    }

    private final RestClient serverClient;
    private final String tableName;
    private final Class<R[]> readArrayClass;

    @Getter
    private final ListDataProvider<R> dataProvider = new ListDataProvider<>(new ArrayList<>());
    private static final String API_PATH = "/api/";

}
