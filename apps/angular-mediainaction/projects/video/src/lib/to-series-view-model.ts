import { SeriesDto } from './proxy/video-service/series-ns';
import { SeriesViewModel } from './series-view-model';

const mapItem = (x: SeriesDto): SeriesViewModel => {
  const orderTotal = x.seriesAliasDtos?.reduce((acc, curr) => ( acc + 0), 0) || 0;
  return {...x, orderTotal};
};
export const toSeriesViewModel = (series: SeriesDto[]) => series.map(mapItem);
