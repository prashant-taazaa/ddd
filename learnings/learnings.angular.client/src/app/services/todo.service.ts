import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  private apiUrl = `${environment.todoWebApiBaseUri}/tasks`;

  constructor(
    private _httpClient: HttpClient,
    private _oidcSecurityService: OidcSecurityService
  ) {}

  CreateTask(description: string): Promise<any> {
    return new Promise((resolve, reject) => {
      const userId = '258c8274-eda4-47fc-9905-fbd265660539';

      this._httpClient
        .post(this.apiUrl, { description, userId }, this.headers())
        .subscribe(
          (response: any) => {
            resolve(response);
          },
          (error: any) => {
            reject(error);
          }
        );
    });
  }

  private headers() {
    let headers = new HttpHeaders({
      Authorization: 'Bearer ' + this._oidcSecurityService.getToken(),
      'Content-Type': 'application/json',
    });
    return { headers: headers };
  }
}
