﻿import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//Angular Material modules
import { MatMenuModule, MatButtonModule, MatIconModule, MatCardModule, MatSidenavModule, MatToolbarModule, MatListModule, MatCheckboxModule, MatSelectModule, MatInputModule, MatStepperModule, MatAutocompleteModule, MatTableModule, MatRadioModule, MatTabsModule } from '@angular/material';

import { ConsumerService } from "./services/consumer.service";
import { ProviderService } from "./services/provider.service";

import { AddressFormComponent } from "./components/provider/consumer-management/address-form/address-form.component";
import { AutocompleteComponent } from "./components/controls/autocomplete/autocomplete.component";
import { ConsumerListComponent } from "./components/provider/consumer-management/consumer-list/consumer-list.component";
import { ConsumerManagementComponent } from "./components/provider/consumer-management/consumer-management.component";
import { DashboardComponent } from "./components/provider/dashboard/dashboard.component";
import { HouseholdComponent } from "./components/provider/consumer-management/household/household.component";
import { HouseholdFormComponent } from "./components/provider/consumer-management/household/household-form/household-form.component";
import { LoginFormComponent } from "./components/provider/consumer-management/user-form/login-form/login-form.component";
import { OrganizationComponent } from "./components/provider/consumer-management/organization/organization.component";
import { ProviderComponent } from './components/provider/provider/provider.component';
import { ProviderFormComponent } from "./components/provider/provider-form/provider-form.component";
import { RegistrationFormComponent } from "./components/provider/consumer-management/user-form/registration-form/registration-form.component";
import { UserFormComponent } from "./components/provider/consumer-management/user-form/user-form.component";

import { EqualValidator } from "./directives/equal-validator.directive";

const consumersRoutes: Routes = [
    { path: '', redirectTo: 'household', pathMatch: 'full' },
    { path: 'household', component: HouseholdComponent },
    { path: 'list', component: ConsumerListComponent },
    { path: 'organization', component: OrganizationComponent }
];

export const providerRoutes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'profile', component: ProviderFormComponent },
    { path: 'consumers', component: ConsumerManagementComponent, children: consumersRoutes }
];


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        MatAutocompleteModule,
        MatButtonModule,
        MatCardModule,
        MatCheckboxModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatMenuModule,
        MatRadioModule,
        MatSelectModule,
        MatSidenavModule,
        MatStepperModule,
        MatTableModule,
        MatTabsModule,
        MatToolbarModule
    ],
    declarations: [
        AddressFormComponent,
        AutocompleteComponent,
        ConsumerListComponent,
        ConsumerManagementComponent,
        DashboardComponent,
        EqualValidator,
        HouseholdComponent,
        HouseholdFormComponent,
        LoginFormComponent,
        OrganizationComponent,
        ProviderComponent,
        ProviderFormComponent,
        RegistrationFormComponent,
        UserFormComponent
    ],
    exports: [ProviderComponent]
})
export class ProviderModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ProviderModule,
            providers: [
                ProviderService,
                ConsumerService
            ]
        }
    }
}