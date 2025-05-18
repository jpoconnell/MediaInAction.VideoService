import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'MediaInAction',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'http://localhost:8080/realms/master',
    redirectUri: baseUrl,
    clientId: 'Web',
    responseType: 'code',
    scope: 'offline_access openid profile email phone roles AdministrationService IdentityService EmbyService DelugeService FileService VideoService TraktService CmskitService', 
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44356',
      rootNamespace: 'MediaInAction.VideoService',
    },
  },
} as Environment;
