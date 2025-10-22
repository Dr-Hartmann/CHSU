package com.client.dashboard;

import com.vaadin.flow.component.AttachEvent;
import com.vaadin.flow.component.DetachEvent;
import com.vaadin.flow.component.charts.Chart;
import com.vaadin.flow.component.charts.model.*;
import com.vaadin.flow.component.charts.model.style.SolidColor;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;

import java.lang.management.ManagementFactory;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class LoadGauge extends VerticalLayout {

    private final ListSeries series;
    private transient ScheduledExecutorService executor;

    public LoadGauge() {
        var chart = new Chart(ChartType.SOLIDGAUGE);
        chart.setWidth("400px");

        var conf = chart.getConfiguration();
        conf.setTitle("Загрузка CPU");

        var pane = conf.getPane();
        pane.setStartAngle(-90);
        pane.setEndAngle(90);

        var background = new Background();
        background.setInnerRadius("60%");
        background.setOuterRadius("100%");
        background.setShape(BackgroundShape.ARC);
        pane.setBackground(background);

        var yAxis = conf.getyAxis();
        yAxis.setMin(0);
        yAxis.setMax(100);

        yAxis.setPlotBands(new PlotBand(0, 50, SolidColor.GREEN),
                new PlotBand(50, 80, SolidColor.YELLOW),
                new PlotBand(80, 100, SolidColor.RED));

        series = new ListSeries("Загрузка", 0);
        conf.addSeries(series);

        add(chart);
    }

    @Override
    protected void onAttach(AttachEvent attachEvent) {
        var ui = attachEvent.getUI();
        executor = Executors.newSingleThreadScheduledExecutor();
        executor.scheduleAtFixedRate(() -> {
            var newValue = getSystemCpuLoad();
            ui.access(() -> series.updatePoint(0, newValue));
        }, 0, 1, TimeUnit.SECONDS);
    }

    @Override
    protected void onDetach(DetachEvent detachEvent) {
        if (executor != null) {
            executor.shutdown();
        }
    }

    private double getSystemCpuLoad() {
        var osBean = ManagementFactory.getPlatformMXBean(com.sun.management.OperatingSystemMXBean.class);
        double load = osBean.getCpuLoad();
        if (load < 0) return 0;
        return load * 100;
    }

}
