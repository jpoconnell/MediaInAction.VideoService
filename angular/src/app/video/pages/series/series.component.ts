import { ListService } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { eVideoPolicyNames } from '../../../proxy/config/src/public-api';
import { finalize } from 'rxjs/operators';
import { SeriesDto } from '../../../proxy/series-ns/dtos';
import { SeriesService } from '../../../proxy/series-ns/series.service';

@Component({
  selector: 'app-series',
  templateUrl: './series.component.html',
  styleUrls: ['./series.component.css'],
  providers: [ListService],
})
export class SeriesComponent implements OnInit {
  permissions = {
    create: eVideoPolicyNames.SeriesManagementCreate,
    update: eVideoPolicyNames.SeriesManagementUpdate,
    delete: eVideoPolicyNames.SeriesManagementDelete,
  };

  items: SeriesDto[] = [];
  count = 0;

  selected: SeriesDto;

  isModalVisible: boolean;

  modalBusy = false;

  form: UntypedFormGroup;
  constructor(
    public readonly list: ListService,
    private readonly service: SeriesService,
    private confirmationService: ConfirmationService,
    private fb: UntypedFormBuilder
  ) {
    this.list.maxResultCount = 10;
  }

  ngOnInit(): void {
    const seriesStreamCreator = query => this.service.getListPaged(query);

    this.list.hookToQuery(seriesStreamCreator).subscribe(response => {
      this.items = response.items;
      this.count = response.totalCount;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selected.name, { validators: [Validators.required] }],
      firstAiredYear: [this.selected.firstAiredYear, { validators: [Validators.required] }],
      imageName: [this.selected.imageName, { validators: [Validators.required] }],
      isActive: [this.selected.isActive, { validators: [Validators.required] }],
  
    });
  }

  onEdit(series: SeriesDto) {
    this.selected = series;
    this.openModal();
  }

  onCreate() {
    this.selected = {} as SeriesDto;
    this.openModal();
  }

  openModal() {
    this.isModalVisible = true;
    this.buildForm();
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
