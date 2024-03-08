import { ModuleWithProviders, NgModule } from '@angular/core';
import { VIDEO_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class VideoConfigModule {
  static forRoot(): ModuleWithProviders<VideoConfigModule> {
    return {
      ngModule: VideoConfigModule,
      providers: [VIDEO_ROUTE_PROVIDERS],
    };
  }
}
