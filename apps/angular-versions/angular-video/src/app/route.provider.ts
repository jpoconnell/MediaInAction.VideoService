import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/video-service',
        name: '::Menu:VideoService',
        iconClass: 'fas fa-book',
        order: 2,
        layout: eLayoutType.application,
      },
      {
          path: '/seriess',
          name: '::Menu:Series',
          parentName: '::Menu:VideoService',
          layout: eLayoutType.application,
          requiredPolicy: 'MediaInAction.Series',
      },
      {
          path: '/movies',
          name: '::Menu:Movies',
          parentName: '::Menu:VideoService',
          layout: eLayoutType.application,
          requiredPolicy: 'MediaInAction.Movies',
      }
    ]);
  };
}
