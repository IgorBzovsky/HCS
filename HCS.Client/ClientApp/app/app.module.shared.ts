import { AdminModule, adminRoutes } from "./admin.module";
import { AuthModule } from "./auth.module";
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { PortalModule, portalRoutes } from "./portal.module";
import { ProviderModule, providerRoutes } from "./provider.module";
import { RouterModule } from '@angular/router';
import { ToastModule } from 'ng2-toastr/ng2-toastr';

//Angular Material modules
import { MatMenuModule, MatButtonModule, MatIconModule, MatCardModule, MatSidenavModule } from '@angular/material';

import { AuthGuardService } from "./services/auth-guard.service";
import { AdminGuardService } from "./services/admin-guard.service";
import { LocationService } from "./services/location.service";
import { ProviderGuardService } from "./services/provider-guard.service";
import { RegexService } from "./services/regex.service";
import { SettingsService } from "./services/settings.service";

import { AboutComponent } from './components/about/about.component';
import { AdminComponent } from "./components/admin/admin/admin.component";
import { AppComponent } from './components/app/app.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { HomeComponent } from './components/home/home.component';
import { PortalComponent } from "./components/portal/portal/portal.component";
import { ProviderComponent } from "./components/provider/provider/provider.component";
import { SilentCallbackComponent } from "./components/silent-callback/silent-callback.component";
import { ConsumersResolveService } from "./services/consumers.resolve.service";


@NgModule({
    declarations: [
        AboutComponent,
        AppComponent,
        AuthCallbackComponent,
        HomeComponent,
        SilentCallbackComponent
    ],
    imports: [
        AuthModule.forRoot(),
        AdminModule.forRoot(),
        BrowserModule,
        BrowserAnimationsModule,
        CommonModule,
        FormsModule,
        HttpModule,
        MatButtonModule,
        MatCardModule,
        MatIconModule,
        MatMenuModule,
        MatSidenavModule,
        PortalModule.forRoot(),
        ProviderModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'about', pathMatch: 'full' },
            { path: 'home', component: HomeComponent, canActivate: [AuthGuardService] },
            { path: 'about', component: AboutComponent },
            {
                path: 'portal',
                component: PortalComponent,
                canActivate: [AuthGuardService],
                resolve: { consumers: ConsumersResolveService },
                children: portalRoutes
            },
            {
                path: 'admin',
                component: AdminComponent,
                canActivate: [AdminGuardService],
                children: adminRoutes
            },
            {
                path: 'provider',
                component: ProviderComponent,
                canActivate: [ProviderGuardService],
                children: providerRoutes
            },
            { path: 'auth-callback', component: AuthCallbackComponent },
            { path: 'silent-callback', component: SilentCallbackComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        ToastModule.forRoot(),
    ],
    providers: [
        LocationService,
        SettingsService,
        RegexService
    ]
})
export class AppModuleShared {
}
