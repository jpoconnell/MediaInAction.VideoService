import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { SeriesRoutingModule } from './series-routing.module';
import { SeriesComponent } from './series.component';

@NgModule({
  declarations: [SeriesComponent],
  imports: [
    CommonModule,
    SeriesRoutingModule,
    ThemeSharedModule,
    CoreModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
  ],
})
export class SeriesModule {}
