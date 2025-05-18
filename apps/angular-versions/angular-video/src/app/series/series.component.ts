import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { SeriesService, SeriesDto } from '@proxy/seriess';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-series',
  templateUrl: './series.component.html',
  styleUrls: ['./series.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class SeriesComponent implements OnInit {
  series = { items: [], totalCount: 0 } as PagedResultDto<SeriesDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedSeries = {} as SeriesDto;

  constructor(
    public readonly list: ListService,
    private seriesService: SeriesService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    const seriesStreamCreator = (query) => this.seriesService.getList(query);

    this.list.hookToQuery(seriesStreamCreator).subscribe((response) => {
      this.series = response;
    });
  }

  createSeries() {
    this.selectedSeries = {} as SeriesDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editSeries(id: string) {
    this.seriesService.get(id).subscribe((series) => {
      this.selectedSeries = series;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedSeries.name || '', Validators.required],
      birthDate: [
        this.selectedSeries.birthDate ? new Date(this.selectedSeries.birthDate) : null,
        Validators.required,
      ],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    if (this.selectedSeries.id) {
      this.seriesService
        .update(this.selectedSeries.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.seriesService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
        .subscribe((status) => {
          if (status === Confirmation.Status.confirm) {
            this.seriesService.delete(id).subscribe(() => this.list.get());
          }
	    });
  }
}
