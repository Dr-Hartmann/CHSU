package com.client.dashboard;

import com.client.service.InventoryService;
import com.vaadin.flow.component.charts.Chart;
import com.vaadin.flow.component.charts.model.*;
import com.vaadin.flow.component.orderedlayout.VerticalLayout;

import java.util.stream.Collectors;

public class PieChart extends VerticalLayout {

    public PieChart(InventoryService inventoryService) {
        var pieChart = new Chart(ChartType.PIE);
        var conf = pieChart.getConfiguration();
        conf.setTitle("Доля производителей");

        var counts = inventoryService.getDataList().stream()
                .collect(Collectors.groupingBy(e -> e.equipment().manufacturerName(), Collectors.counting()));

        var series = new DataSeries();
        counts.forEach((name, count) -> series.add(new DataSeriesItem(name, count)));
        conf.setSeries(series);

        var tooltip = new Tooltip();
        tooltip.setPointFormat("Количество: <b>{point.y} шт.</b>");
        conf.setTooltip(tooltip);

        var plotOptions = new PlotOptionsColumn();
        var labels = new DataLabels(true);
        plotOptions.setDataLabels(labels);
        plotOptions.setCursor(Cursor.POINTER);
        conf.setPlotOptions(plotOptions);

        add(pieChart);
    }

}
