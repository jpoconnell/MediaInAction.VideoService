import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-series-detail-item',
  standalone: false,
  template: `
    <div class="row">
      <div class="col-3">
        {{ label | abpLocalization }}
      </div>
      <b class="col-9">
        <ng-content></ng-content>
      </b>
    </div>
  `,
  styles: [],
})
export class SeriesDetailItemComponent {
  @Input()
  label = '';
}
