import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eFilePolicyNames } from '../enums/policy-names';
import { eFileRouteNames } from '../enums/route-names';

export const FILE_ROUTE_PROVIDERS = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

export function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/file',
        name: eFileRouteNames.File,
        layout: eLayoutType.application,
        parentName: null,
        iconClass: 'bi bi-collection-fill',
        requiredPolicy: eFilePolicyNames.File,
      },
      {
        path: '/file/file-entrys',
        name: eFileRouteNames.Products,
        parentName: eFileRouteNames.File,
        order: 1,
        requiredPolicy: eFilePolicyNames.FileEntryManagement,
        iconClass: 'bi bi-tag-fill',
      },
    ]);
  };
}
