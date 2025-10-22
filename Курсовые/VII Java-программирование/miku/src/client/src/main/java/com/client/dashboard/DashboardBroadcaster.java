package com.client.dashboard;

import com.vaadin.flow.shared.Registration;

import java.util.Map;
import java.util.Optional;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.function.Consumer;

public class DashboardBroadcaster {

    private static final Map<String, CopyOnWriteArrayList<Consumer<Long>>> channels = new ConcurrentHashMap<>();

    public static Registration register(String channelId, Consumer<Long> listener) {
        channels.computeIfAbsent(channelId, _ -> new CopyOnWriteArrayList<>()).add(listener);
        return () -> channels.getOrDefault(channelId, new CopyOnWriteArrayList<>()).remove(listener);
    }

    public static void broadcast(String channelId, Long count) {
        Optional.ofNullable(channels.get(channelId)).ifPresent(listeners ->
                listeners.forEach(l -> l.accept(count)));
    }

    private DashboardBroadcaster() {
    }

}
