import { Component } from '@angular/core';
import { SeriesService } from '../../lib/proxy/video-service/series-ns';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent {
  constructor(private service: SeriesService) {}

  dashboard$ = this.service.getDashboard({});
}
