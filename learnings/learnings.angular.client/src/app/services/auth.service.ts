import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  authApi = environment.stsServerUri + '/api/account';

  constructor(
    private _httpClient: HttpClient,
    public oidcSecurityService: OidcSecurityService
  ) {}

  register(userRegistration: any) {
    return this._httpClient.post(this.authApi, userRegistration);
  }

  signIn() {
    this.oidcSecurityService.authorize();
  }
}
