import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { ProtectedComponent } from './components/protected/protected.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { AboutComponent } from './components/about/about.component';

import { TestService } from './services/test.service';
import { AuthModule } from "./auth.module";
import { AuthGuardService } from "./services/auth-guard.service";

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
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'protected', component: ProtectedComponent, canActivate: [AuthGuardService] },
            { path: 'auth-callback', component: AuthCallbackComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        TestService]
})
export class AppModuleShared {
}
