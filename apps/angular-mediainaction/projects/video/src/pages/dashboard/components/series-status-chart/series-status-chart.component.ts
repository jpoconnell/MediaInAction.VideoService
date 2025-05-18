import { Component, Input } from '@angular/core';
import { SeriesStatusDto } from '../../../../lib/proxy/video-service/series-ns';

@Component({
  selector: 'app-series-status-chart',
  templateUrl: './series-status-chart.component.html',
})
export class SeriesStatusChartComponent {
  @Input()
  set data(value: SeriesStatusDto[]) {
    const filtered = value.filter(x => x.countOfStatusSeries > 0);
    this.chartData.datasets = [
      {
        data: [...filtered.map(x => x.countOfStatusSeries)],
        backgroundColor: ['#fdcb6e', '#0984e3', '#ff7675'],
      },
    ];
    this.chartData.labels = [...filtered.map(x => x.seriesStatus)];
  }

  chartData = {
    labels: [],
    datasets: [],
  };

  options = {
    plugins: {
      title: {
        display: false,
        text: '',
        fontSize: 16,
      },
      legend: {
        position: 'bottom',
      },
    },
  };
}
