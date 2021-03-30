import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AuthModule, OidcConfigService } from 'angular-auth-oidc-client';

export function configureAuth(oidcConfigService: OidcConfigService): () => Promise<any> {
    return () =>
        oidcConfigService.withConfig({
            stsServer: 'https://localhost:5001',
            redirectUrl: window.location.origin,
            postLogoutRedirectUri: window.location.origin,
            clientId: 'angular',
            scope: 'openid profile email',
            responseType: 'code',
            silentRenew: true,
            silentRenewUrl: window.location.origin + '/silent-renew.html', 
            renewTimeBeforeTokenExpiresInSeconds: 10,
        });
}

@NgModule({
    imports: [AuthModule.forRoot()],
    exports: [AuthModule],
    providers: [
        OidcConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: configureAuth,
            deps: [OidcConfigService],
            multi: true,
        },
    ],
})
export class AuthConfigModule { }
