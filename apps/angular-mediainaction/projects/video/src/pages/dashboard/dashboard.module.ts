import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileRoutingModule } from '@mediainaction/file';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CoreModule } from '@abp/ng.core';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { SeriesStatusChartComponent } from './components/series-status-chart/series-status-chart.component';
import { ChartModule } from '@abp/ng.components/chart.js';


@NgModule({
  declarations: [DashboardComponent, SeriesStatusChartComponent],
  imports: [
    DashboardRoutingModule,
    CommonModule,
    FileRoutingModule,
    CommonModule,
    ThemeSharedModule,
    CoreModule,
    ChartModule,
  ],
})
export class DashboardModule {}
