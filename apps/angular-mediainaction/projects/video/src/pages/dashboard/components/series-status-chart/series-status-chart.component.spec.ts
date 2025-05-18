import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeriesStatusChartComponent } from './series-status-chart.component';

describe('SeriesStatusChartComponent', () => {
  let component: SeriesStatusChartComponent;
  let fixture: ComponentFixture<SeriesStatusChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeriesStatusChartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeriesStatusChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
