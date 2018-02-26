import { AdminModule, adminRoutes } from "./admin.module";
import { ProviderModule, providerRoutes } from "./provider.module";
import { AuthModule } from "./auth.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { PortalModule, portalRoutes } from "./portal.module";
import { RouterModule } from '@angular/router';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { MdMenuModule, MdButtonModule, MdIconModule, MdCardModule, MdSidenavModule } from '@angular/material';

import { AboutComponent } from './components/about/about.component';
import { AdminComponent } from "./components/admin/admin/admin.component";
import { AppComponent } from './components/app/app.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { HomeComponent } from './components/home/home.component';
import { PortalComponent } from "./components/portal/portal/portal.component";

import { AuthGuardService } from "./services/auth-guard.service";
import { AdminGuardService } from "./services/admin-guard.service";
import { ProviderGuardService } from "./services/provider-guard.service";
import { SettingsService } from "./services/settings.service";
import { TestService } from './services/test.service';
import { ProviderComponent } from "./components/provider/provider/provider.component";



@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        AuthCallbackComponent,
        AboutComponent
    ],
    imports: [
        AuthModule.forRoot(),
        AdminModule.forRoot(),
        ProviderModule.forRoot(),
        ToastModule.forRoot(),
        PortalModule,
        CommonModule,
        BrowserAnimationsModule,
        HttpModule,
        FormsModule,
        MdMenuModule,
        MdButtonModule,
        MdIconModule,
        MdCardModule,
        MdSidenavModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'about', pathMatch: 'full' },
            { path: 'home', component: HomeComponent, canActivate: [AuthGuardService] },
            { path: 'about', component: AboutComponent },
            {
                path: 'portal',
                component: PortalComponent,
                canActivate: [AuthGuardService],
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
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [TestService, SettingsService]
})
export class AppModuleShared {
}
