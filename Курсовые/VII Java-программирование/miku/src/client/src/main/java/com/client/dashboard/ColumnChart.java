package com.client.dashboard;

import com.client.service.EquipmentService;
import com.vaadin.flow.component.charts.Chart;
import com.vaadin.flow.component.charts.model.*;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;

import java.util.Map;
import java.util.stream.Collectors;

public class ColumnChart extends VerticalLayout {

    public ColumnChart(EquipmentService equipmentService) {
        var topManufacturers = equipmentService.getDataList().stream()
                .collect(Collectors.groupingBy(e -> e.manufacturer().name(), Collectors.counting()))
                .entrySet().stream()
                .sorted(Map.Entry.<String, Long>comparingByValue().reversed())
                .limit(5)
                .collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));

        var columnChart = new Chart(ChartType.COLUMN);
        var conf = columnChart.getConfiguration();
        conf.setTitle("Топ-5 производителей по количеству оборудования на рынке");

        var xAxis = new XAxis();
        xAxis.setCategories(topManufacturers.keySet().toArray(new String[0])); // Имена на ось X
        conf.addxAxis(xAxis);

        var yAxis = conf.getyAxis();
        yAxis.setTitle("Количество единиц");

        var series = new ListSeries("Оборудование");
        topManufacturers.values().forEach(series::addData);
        conf.addSeries(series);

        var tooltip = new Tooltip();
        tooltip.setPointFormat("Количество: <b>{point.y} шт.</b>");
        conf.setTooltip(tooltip);

        var plotOptions = new PlotOptionsColumn();
        var labels = new DataLabels(true);
        plotOptions.setDataLabels(labels);
        plotOptions.setCursor(Cursor.POINTER);
        conf.setPlotOptions(plotOptions);

        add(columnChart);
    }

}
