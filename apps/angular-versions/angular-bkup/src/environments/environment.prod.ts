import { MyEnvironment } from './my-environment';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'MediaInAction',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44394',
    redirectUri: baseUrl,
    clientId: 'Web',
    responseType: 'code',
    scope: 'offline_access openid profile email phone AccountService IdentityService AdministrationService FileService VideoService',
  },
  apis: {
    default: {
      url: 'https://localhost:44394',
      rootNamespace: 'MediaInAction',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as MyEnvironment;
