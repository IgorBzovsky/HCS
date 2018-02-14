import { AdminModule, adminRoutes } from "./admin.module";
import { AuthModule } from "./auth.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { PortalModule, portalRoutes } from "./portal.module";
import { RouterModule } from '@angular/router';
import { ToastModule } from 'ng2-toastr/ng2-toastr';

import { AboutComponent } from './components/about/about.component';
import { AdminComponent } from "./components/admin/admin/admin.component";
import { AppComponent } from './components/app/app.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PortalComponent } from "./components/portal/portal/portal.component";
import { ProtectedComponent } from './components/protected/protected.component';

import { AuthGuardService } from "./services/auth-guard.service";
import { TestService } from './services/test.service';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        ProtectedComponent,
        AuthCallbackComponent,
        AboutComponent
    ],
    imports: [
        AuthModule.forRoot(),
        AdminModule.forRoot(),
        ToastModule.forRoot(),
        PortalModule,
        CommonModule,
        BrowserAnimationsModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'about', pathMatch: 'full' },
            { path: 'home', component: HomeComponent, canActivate: [AuthGuardService] },
            { path: 'about', component: AboutComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'protected', component: ProtectedComponent, canActivate: [AuthGuardService] },
            {
                path: 'portal',
                component: PortalComponent,
                canActivate: [AuthGuardService],
                children: portalRoutes
            },
            {
                path: 'admin',
                component: AdminComponent,
                canActivate: [AuthGuardService],
                children: adminRoutes
            },
            { path: 'auth-callback', component: AuthCallbackComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [TestService]
})
export class AppModuleShared {
}
