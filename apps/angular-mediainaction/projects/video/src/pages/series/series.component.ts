import { Component, OnInit } from '@angular/core';
import { SeriesService } from '../../lib/proxy/video-service/series-ns';
import { SeriesViewModel, toSeriesViewModel } from '../../lib';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { ListService } from '@abp/ng.core';
import { eVideoPolicyNames } from '@mediainaction/video/config';

@Component({
  selector: 'app-series',
  standalone: false,
  templateUrl: './series.component.html',
  providers: [ListService],
})
export class SeriesComponent implements OnInit {
  constructor(
    private service: SeriesService,
    public list: ListService,
    private confirmationService: ConfirmationService
  ) {}

  selectedSeries: SeriesViewModel | undefined;
  isModalVisible = false;
  items: SeriesViewModel[];
  count = 0;
  permissions = {
    detail: eVideoPolicyNames.video,
    setAsShipped: eVideoPolicyNames.setAsShipped,
    setAsCancelled: eVideoPolicyNames.setAsCancelled,
  };

  ngOnInit(): void {
    const seriesStreamCreator = query => this.service.getListPaged(query);

    this.list.hookToQuery(seriesStreamCreator).subscribe(response => {
      this.items = toSeriesViewModel(response.items);
      this.count = response.totalCount;
    });
  }

  openModal(series: SeriesViewModel) {
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

  setAsInActive(row: SeriesViewModel) {
    this.confirmationService
      .warn('AbpVideo::WillSetAsShipped', { key: '::AreYouSure', defaultValue: 'Are you sure?' })
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
