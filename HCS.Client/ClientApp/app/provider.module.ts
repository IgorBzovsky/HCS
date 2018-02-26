import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { MdMenuModule, MdButtonModule, MdIconModule, MdCardModule, MdSidenavModule, MdToolbarModule, MdListModule, MdCheckboxModule, MdSelectModule, MdInputModule } from '@angular/material';

import { ProviderComponent } from './components/provider/provider/provider.component';
import { ProviderFormComponent } from "./components/provider/provider-form/provider-form.component";

import { ProviderService } from "./services/provider.service";
import { DashboardComponent } from "./components/provider/dashboard/dashboard.component";

export const providerRoutes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'profile', component: ProviderFormComponent }
];


@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        MdMenuModule,
        MdButtonModule,
        MdIconModule,
        MdCardModule,
        MdSidenavModule,
        MdToolbarModule,
        MdListModule,
        MdCheckboxModule,
        MdSelectModule,
        MdInputModule
    ],
    declarations: [
        ProviderComponent,
        ProviderFormComponent,
        DashboardComponent
    ],
    exports: [ProviderComponent]
})
export class ProviderModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ProviderModule,
            providers: [
                ProviderService
            ]
        }
    }
}