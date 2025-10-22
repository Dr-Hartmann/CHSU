package com.client.dashboard;

import com.client.base.BaseRestService;
import lombok.RequiredArgsConstructor;
import org.springframework.scheduling.annotation.EnableScheduling;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@EnableScheduling
@RequiredArgsConstructor
public class DashboardUpdateService {

    private final List<BaseRestService<?, ?, ?>> restServices;

    @Scheduled(fixedRate = 5000)
    public void updateAll() {
        for (var restService : restServices) {
            var channelId = restService.getClass().getSimpleName();
            DashboardBroadcaster.broadcast(channelId, restService.getDataSize());
        }
    }

}
