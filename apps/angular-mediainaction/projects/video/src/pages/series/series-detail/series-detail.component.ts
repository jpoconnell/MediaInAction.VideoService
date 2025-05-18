import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SeriesViewModel } from '../../../lib/series-view-model';
import { environment } from '../../../../../../src/environments/environment';

@Component({
  selector: 'app-series-detail',
  standalone: false,
  templateUrl: './series-detail.component.html',
})
export class SeriesDetailComponent {
  modalOption = { size: 'xl' };
  @Input()
  visible: boolean;
  @Input()
  series: SeriesViewModel | undefined;
  mediaServerUrl = environment.mediaServerUrl;
  @Output() readonly visibleChange = new EventEmitter<boolean>();
}
