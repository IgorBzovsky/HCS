import { NgModule, ModuleWithProviders } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';
import { AuthHttp, AuthConfig } from 'angular2-jwt';
import { AppSettingsService } from './services/app-settings.service';
import { AuthGuardService } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';

let authService = new AuthService();

export function authHttpServiceFactory(http: Http, options: RequestOptions) {
    return new AuthHttp(new AuthConfig({
        tokenGetter: (() => authService.getAccessToken())
    }), http, options);
}

@NgModule({
})
export class AuthModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: AuthModule,
            providers: [
                AuthGuardService,
                AuthService,
                AppSettingsService,
                {
                    provide: AuthHttp,
                    useFactory: authHttpServiceFactory,
                    deps: [Http, RequestOptions]
                }
            ]
        }
    }
}