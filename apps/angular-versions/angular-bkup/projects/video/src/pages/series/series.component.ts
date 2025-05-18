import { Component, OnInit } from '@angular/core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { ListService } from '@abp/ng.core';
import { eVideoPolicyNames } from '@mediainaction/video/config';
import { SeriesDto } from '../../lib/proxy/video-service/series-ns/models';
import { SeriesService } from '../../lib/proxy/video-service/series-ns/series.service';


@Component({
  selector: 'app-series',
  templateUrl: './series.component.html',
  providers: [ListService],
})
export class SeriesComponent implements OnInit {
  constructor(
    private service: SeriesService,
    public list: ListService,
    private confirmationService: ConfirmationService
  ) {}

  selectedSeries: SeriesDto | undefined;
  isModalVisible = false;
  items: SeriesDto[];
  count = 0;
  permissions = {
    detail: eVideoPolicyNames.video,
    setAsInActive: eVideoPolicyNames.setAsInActive,
  };

  ngOnInit(): void {
    const seriesStreamCreator = query => this.service.getSeriesListPaged(query);

    this.list.hookToQuery(seriesStreamCreator).subscribe(response => {
      this.items = response.items;
      this.count = response.totalCount;
    });
  }

  openModal(series: SeriesDto) {
    if (!series) {
      return;
    }
    this.selectedSeries = series;
    this.isModalVisible = true;
  }

  closeModal(isVisible: boolean) {
    if (isVisible) {
      return;
    }
    this.selectedSeries = null;
    this.isModalVisible = false;
  }

  setAsInActive(row: SeriesDto) {
    this.confirmationService
      .warn('AbpVideo::WillSetAsInActive', {
        key: '::AreYouSure',
        defaultValue: 'Are you sure?',
      })
      .subscribe(status => {
        if (status !== Confirmation.Status.confirm) {
          return;
        }
        this.service.setAsInActive(row.id).subscribe(() => {
          this.list.get();
        });
      });
  }
}
