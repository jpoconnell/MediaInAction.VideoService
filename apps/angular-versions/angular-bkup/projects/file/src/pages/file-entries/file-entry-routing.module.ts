import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { eFilePolicyNames } from '@mediainaction/file/config';
import { FileEntryComponent } from './file-entry.component';

const routes: Routes = [
  {
    path: '',
    component: FileEntryComponent,
    canActivate: [AuthGuard, PermissionGuard],
    data: {
      requiredPolicy: eFilePolicyNames.FileEntryManagement,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FileEntryRoutingModule {}
