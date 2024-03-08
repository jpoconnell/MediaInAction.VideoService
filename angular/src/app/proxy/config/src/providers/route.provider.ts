import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eVideoPolicyNames } from '../enums/policy-names';
import { eVideoRouteNames } from '../enums/route-names';

export const VIDEO_ROUTE_PROVIDERS = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

export function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/video',
        name: eVideoRouteNames.Video,
        layout: eLayoutType.application,
        parentName: null,
        iconClass: 'bi bi-collection-fill',
        requiredPolicy: eVideoPolicyNames.Video,
      },
      {
        path: '/video/seriess',
        name: eVideoRouteNames.Series,
        parentName: eVideoRouteNames.Video,
        order: 1,
        requiredPolicy: eVideoPolicyNames.SeriesManagement,
        iconClass: 'bi bi-tag-fill',
      },
    ]);
  };
}
