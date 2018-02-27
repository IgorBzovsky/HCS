import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MdMenuModule, MdButtonModule, MdIconModule, MdCardModule, MdSidenavModule, MdToolbarModule, MdListModule, MdCheckboxModule, MdSelectModule, MdInputModule, MdAutocompleteModule } from '@angular/material';

import { ProviderService } from "./services/provider.service";

import { ProviderComponent } from './components/provider/provider/provider.component';
import { ProviderFormComponent } from "./components/provider/provider-form/provider-form.component";
import { DashboardComponent } from "./components/provider/dashboard/dashboard.component";
import { HouseholdFormComponent } from "./components/provider/household-form/household-form.component";
import { AutocompleteComponent } from "./components/controls/autocomplete/autocomplete.component";

export const providerRoutes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'profile', component: ProviderFormComponent },
    { path: 'household/new', component: HouseholdFormComponent }
];


@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        MdMenuModule,
        MdButtonModule,
        MdIconModule,
        MdCardModule,
        MdSidenavModule,
        MdToolbarModule,
        MdListModule,
        MdCheckboxModule,
        MdSelectModule,
        MdInputModule,
        MdAutocompleteModule
    ],
    declarations: [
        ProviderComponent,
        ProviderFormComponent,
        DashboardComponent,
        HouseholdFormComponent,
        AutocompleteComponent
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