import { MyEnvironment } from './my-environment';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'MediaInAction',
  },
  oAuthConfig: {
    issuer: 'http://localhost:8080/realms/master',
    redirectUri: baseUrl,
    clientId: 'Web',
    responseType: 'code',
    scope: 'offline_access openid profile email phone roles AdministrationService IdentityService EmbyService DelugeService FileService VideoService TraktService CmskitService', 
    //requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44372',
      rootNamespace: 'MediaInAction',
    },
    File: {
      url: 'https://localhost:44355',
      rootNamespace: 'MediaInAction.FileService',
    },
    Video: {
      url: "https://localhost:44356",
      rootNamespace: 'MediaInAction.VideoService',
    }
  },
  mediaServerUrl:'https://localhost:44373'
} as MyEnvironment;


