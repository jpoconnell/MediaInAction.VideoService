import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { eVideoPolicyNames } from '../../../proxy/config/src/enums/policy-names';
import { SeriesComponent } from './series.component';

const routes: Routes = [
  {
    path: '',
    component: SeriesComponent,
    canActivate: [AuthGuard, PermissionGuard],
    data: {
      requiredPolicy: eVideoPolicyNames.SeriesManagement,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SeriesRoutingModule {}
