import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SeriesRoutingModule } from './series-routing.module';
import { SeriesComponent } from './series.component';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CoreModule } from '@abp/ng.core';
import { SeriesDetailComponent } from './series-detail/series-detail.component';
import { SeriesDetailItemComponent } from './series-detail/series-detail-item/series-detail-item.component';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [SeriesComponent, SeriesDetailComponent, SeriesDetailItemComponent],
  imports: [CommonModule, NgbDropdownModule, SeriesRoutingModule, ThemeSharedModule, CoreModule],
})
export class SeriesModule {}
