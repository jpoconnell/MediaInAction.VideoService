import { ListService } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { eFilePolicyNames } from '@mediainaction/file/config';
import { finalize } from 'rxjs/operators';
import { FileEntryDto } from '../../lib/proxy/file-service/file-entries-ns/models';
import { FileEntryService } from '../../lib/proxy/file-service/file-entries-ns/file-entry.service';

@Component({
  selector: 'app-file-entry',
  standalone: false,
  templateUrl: './file-entry.component.html',
  styleUrls: ['./file-entry.component.css'],
  providers: [ListService],
})
export class FileEntryComponent implements OnInit {
  permissions = {
    create: eFilePolicyNames.FileEntryManagementCreate,
    update: eFilePolicyNames.FileEntryManagementUpdate,
    delete: eFilePolicyNames.FileEntryManagementDelete,
  };

  items: FileEntryDto[] = [];
  count = 0;

  selected: FileEntryDto;

  isModalVisible: boolean;

  modalBusy = false;

  form: UntypedFormGroup;
  constructor(
    public readonly list: ListService,
    private readonly service: FileEntryService,
    private confirmationService: ConfirmationService,
    private fb: UntypedFormBuilder
  ) {
    this.list.maxResultCount = 10;
  }

  ngOnInit(): void {
    const fileEntryStreamCreator = query => this.service.getListPaged(query);

    this.list.hookToQuery(fileEntryStreamCreator).subscribe(response => {
      this.items = response.items;
      this.count = response.totalCount;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      filename: [this.selected.filename, { validators: [Validators.required] }],
      directory: [this.selected.directory, { validators: [Validators.required] }],
      server: [this.selected.server, { validators: [Validators.required] }],
      size: [this.selected.size, { validators: [Validators.required] }],
      extn: [this.selected.extn, { validators: [Validators.required] }],
    });
  }

  onEdit(fileEntry: FileEntryDto) {
    this.selected = fileEntry;
    this.openModal();
  }

  onCreate() {
    this.selected = {} as FileEntryDto;
    this.openModal();
  }

  openModal() {
    this.isModalVisible = true;
    this.buildForm();
  }

  onDelete(fileEntry: FileEntryDto) {
    this.confirmationService
      .warn('FileService::FileEntryDeletionConfirmationMessage', 'AbpUi::AreYouSure', {
        messageLocalizationParams: [fileEntry.filename],
      })
      .subscribe((status: Confirmation.Status) => {
        if (status === Confirmation.Status.confirm) {
          this.service.delete(fileEntry.id).subscribe(() => this.list.get());
        }
      });
  }

  save() {
    if (!this.form.valid || this.modalBusy) {
      return;
    }
    this.modalBusy = true;

    const { id } = this.selected;
    (id
      ? this.service.update(id, {
          ...this.selected,
          ...this.form.value,
        })
      : this.service.create({ ...this.form.value })
    )
      .pipe(finalize(() => (this.modalBusy = false)))
      .subscribe(() => {
        this.isModalVisible = false;
        this.list.get();
      });
  }
}
