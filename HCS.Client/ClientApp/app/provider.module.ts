import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from './shared.module';

//Angular Material modules
import { MatMenuModule, MatButtonModule, MatIconModule, MatCardModule, MatSidenavModule, MatToolbarModule, MatListModule, MatCheckboxModule, MatSelectModule, MatInputModule, MatStepperModule, MatAutocompleteModule, MatTableModule, MatRadioModule, MatTabsModule, MatSlideToggleModule, MatExpansionModule } from '@angular/material';

import { ConsumerService } from "./services/consumer.service";
import { ProviderService } from "./services/provider.service";
import { TariffService } from "./services/tariff.service";

import { AddressFormComponent } from "./components/provider/consumer-management/address-form/address-form.component";
import { AutocompleteComponent } from "./components/controls/autocomplete/autocomplete.component";
import { ConsumerListComponent } from "./components/provider/consumer-management/consumer-list/consumer-list.component";
import { ConsumerManagementComponent } from "./components/provider/consumer-management/consumer-management.component";
import { DashboardComponent } from "./components/provider/dashboard/dashboard.component";
import { HouseholdComponent } from "./components/provider/consumer-management/household/household.component";
import { HouseholdFormComponent } from "./components/provider/consumer-management/household/household-form/household-form.component";
import { LoginFormComponent } from "./components/provider/consumer-management/user-form/login-form/login-form.component";
import { OccupantsComponent } from "./components/provider/consumer-management/household/occupants/occupants.component";
import { OccupantsFormComponent } from "./components/provider/consumer-management/household/occupants/occupants-form/occupants-form.component";
import { OccupantsListComponent } from "./components/provider/consumer-management/household/occupants/occupants-list/occupants-list.component";
import { OrganizationComponent } from "./components/provider/consumer-management/organization/organization.component";
import { OrganizationFormComponent } from "./components/provider/consumer-management/organization/organization-form/organization-form.component";
import { ProviderComponent } from './components/provider/provider/provider.component';
import { ProviderFormComponent } from "./components/provider/provider-form/provider-form.component";
import { RegistrationFormComponent } from "./components/provider/consumer-management/user-form/registration-form/registration-form.component";
import { SubsidyComponent } from "./components/provider/consumer-management/household/subsidy/subsidy.component";
import { TariffFormComponent } from "./components/provider/tariffs/tariff-form/tariff-form.component";
import { TariffListComponent } from "./components/provider/tariffs/tariff-list/tariff-list.component";
import { UserFormComponent } from "./components/provider/consumer-management/user-form/user-form.component";
import { BlockComponent } from "./components/provider/tariffs/tariff-form/block/block.component";
import { OrganizationTariffComponent } from "./components/provider/consumer-management/organization/organization-tariff/organization-tariff.component";
import { OccupantService } from "./services/occupant.service";
import { HouseholdTariffComponent } from "./components/provider/consumer-management/household/household-tariff/household-tariff.component";


const consumersRoutes: Routes = [
    { path: '', redirectTo: 'household', pathMatch: 'full' },
    { path: 'household', component: HouseholdComponent },
    { path: 'list', component: ConsumerListComponent },
    { path: 'organization', component: OrganizationComponent }
];

export const providerRoutes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'tariffs', component: TariffListComponent },
    { path: 'tariffs/new', component: TariffFormComponent },
    { path: 'tariffs/:id', component: TariffFormComponent },
    { path: 'profile', component: ProviderFormComponent },
    { path: 'consumers', component: ConsumerListComponent },
    { path: 'consumers/organizations/new', component: OrganizationComponent },
    { path: 'consumers/organizations/:id', component: OrganizationComponent },
    { path: 'consumers/households/new', component: HouseholdComponent },
    { path: 'consumers/households/:id', component: HouseholdComponent }
    /*{ path: 'consumers', component: ConsumerManagementComponent, children: consumersRoutes }*/
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
        MatExpansionModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatMenuModule,
        MatRadioModule,
        MatSelectModule,
        MatSidenavModule,
        MatSlideToggleModule,
        MatStepperModule,
        MatTableModule,
        MatTabsModule,
        MatToolbarModule,
        SharedModule
    ],
    declarations: [
        AddressFormComponent,
        AutocompleteComponent,
        BlockComponent,
        ConsumerListComponent,
        ConsumerManagementComponent,
        DashboardComponent,
        HouseholdComponent,
        HouseholdFormComponent,
        HouseholdTariffComponent,
        LoginFormComponent,
        OccupantsComponent,
        OccupantsFormComponent,
        OccupantsListComponent,
        OrganizationComponent,
        OrganizationFormComponent,
        OrganizationTariffComponent,
        ProviderComponent,
        ProviderFormComponent,
        RegistrationFormComponent,
        SubsidyComponent,
        TariffFormComponent,
        TariffListComponent,
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
                ConsumerService,
                OccupantService,
                TariffService
            ]
        }
    }
}