import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { FileEntryRoutingModule } from './file-entry-routing.module';
import { FileEntryComponent } from './file-entry.component';

@NgModule({
  declarations: [FileEntryComponent],
  imports: [
    CommonModule,
    FileEntryRoutingModule,
    ThemeSharedModule,
    CoreModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
  ],
})
export class FileEntryModule {}
