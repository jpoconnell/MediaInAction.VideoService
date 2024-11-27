import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44317/',
  redirectUri: baseUrl,
  clientId: 'VideoService_App',
  responseType: 'code',
  scope: 'offline_access VideoService',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'VideoService',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44317',
      rootNamespace: 'MediaInAction.VideoService',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
