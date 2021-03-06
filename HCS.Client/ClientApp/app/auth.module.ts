﻿import { NgModule, ModuleWithProviders } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';
import { AuthHttp, AuthConfig } from 'angular2-jwt';
import { AuthGuardService } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { AdminGuardService } from "./services/admin-guard.service";
import { ProviderGuardService } from "./services/provider-guard.service";

let authService = new AuthService();

export function authHttpServiceFactory(http: Http, options: RequestOptions) {
    return new AuthHttp(new AuthConfig({
        tokenName: 'token',
        tokenGetter: (() => authService.getAccessToken()),
        globalHeaders: [{ 'Content-Type': 'application/json' }]
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
                AdminGuardService,
                ProviderGuardService,
                AuthService,
                {
                    provide: AuthHttp,
                    useFactory: authHttpServiceFactory,
                    deps: [Http, RequestOptions]
                }
            ]
        }
    }
}