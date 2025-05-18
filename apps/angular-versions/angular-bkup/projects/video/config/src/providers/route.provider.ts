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
        name: eVideoRouteNames.video,
        layout: eLayoutType.application,
        requiredPolicy: eVideoPolicyNames.video,
        parentName: null,
        iconClass: 'bi bi-collection-fill',
      },
      {
        path: '/video',
        name: eVideoRouteNames.dashboard,
        parentName: eVideoRouteNames.video,
        order: 1,
        requiredPolicy: eVideoPolicyNames.video,
      },
      {
        path: '/video/seriess',
        name: eVideoRouteNames.seriess,
        parentName: eVideoRouteNames.video,
        order: 2,
        requiredPolicy: eVideoPolicyNames.video,
      },
    ]);
  };
}
