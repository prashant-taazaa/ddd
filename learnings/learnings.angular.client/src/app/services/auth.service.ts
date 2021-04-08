import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';
import jwt_decode from 'jwt-decode';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  api = environment.todoWebApiBaseUri + '/accounts';

  constructor(
    private _httpClient: HttpClient,
    public oidcSecurityService: OidcSecurityService
  ) {}

  register(userRegistration: any) {
    return this._httpClient.post(`${this.api}/register`, userRegistration);
  }

  signIn(signIn: any) {
    return this._httpClient.post(`${this.api}/login`, signIn);
  }

  isLoggedIn() {
    const claims = this.getClaims();
    if (claims) {
      console.log(claims);
      return true;
    } else {
      return false;
    }
  }

  getToken() {
    return localStorage.getItem('auth-access-token');
  }

  getClaims() {
    const token = this.getToken();
    if (token) {
      return jwt_decode(token);
    }
  }
}
