 import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44390/',
  redirectUri: baseUrl,
  clientId: 'VideoService_App',
  responseType: 'code',
  scope: 'offline_access VideoService',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'VideoService',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44390',
      rootNamespace: 'MediaInAction.VideoService',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
