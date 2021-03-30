import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  private apiBaseUri = 'https://localhost:6001/api';

  constructor(private _httpClient: HttpClient, public oidcSecurityService: OidcSecurityService) {

  }


  ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe((auth) => console.log('is authenticated', auth));
    // this._httpClient.get(`${this.apiBaseUri}/identity`).subscribe((response: any) => {
    //   console.log(response);

    // }, (error: any) => {
    //   console.log(error);

    // })
  }
  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
  }
}
