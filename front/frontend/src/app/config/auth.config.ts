import { AuthConfig } from "angular-oauth2-oidc";


export const authConfig: AuthConfig = {
  issuer: "http://localhost:5001",
  loginUrl:  'http://localhost:5001/connect/authorize',
  logoutUrl: 'http://localhost:5001/Account/logout',
  redirectUri: 'http://localhost:4200/' ,
  clientId: 'client',
  scope: 'api1',
  oidc : true,
  userinfoEndpoint :  'http://localhost:5001/connect/userinfo',
  tokenEndpoint :  'http://localhost:5001/connect/token',
  requestAccessToken: true,
  useSilentRefresh: true,
  silentRefreshRedirectUri: window.location.origin + '/assets/silent-refresh.html',
  useIdTokenHintForSilentRefresh: true
};
