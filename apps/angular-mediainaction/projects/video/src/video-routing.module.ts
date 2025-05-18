import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./pages/dashboard/dashboard.module').then(m => m.DashboardModule),
    pathMatch: 'full',
  },
  {
    path: 'series',
    loadChildren: () => import('./pages/series/series.module').then(m => m.SeriesModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VideoRoutingModule {}
