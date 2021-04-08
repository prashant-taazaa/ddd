import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  loading: boolean = false;

  constructor(private _httpClient: HttpClient,
    public oidcSecurityService: OidcSecurityService,
    private router: Router) {

  }


  ngOnInit(): void {
    // this.loading = true;
    // this.oidcSecurityService.checkAuth().subscribe((auth) => {
    //   console.log('is authenticated', auth);
    //   this.loading = false;
    //   if (!auth) {
    //      this.oidcSecurityService.authorize();
    //   }
    // });

  }
}
