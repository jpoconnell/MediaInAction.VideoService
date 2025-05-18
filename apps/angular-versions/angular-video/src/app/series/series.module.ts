import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { SeriesRoutingModule } from './series-routing.module';
import { SeriesComponent } from './series.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [SeriesComponent],
  imports: [SharedModule, SeriesRoutingModule, NgbDatepickerModule],
})
export class SeriesModule {}
