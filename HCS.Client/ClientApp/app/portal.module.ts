import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { MatMenuModule, MatButtonModule, MatIconModule, MatCardModule, MatSidenavModule, MatToolbarModule, MatListModule, MatInputModule, MatRadioModule, MatSelectModule, MatTableModule } from '@angular/material';

import { PortalComponent } from './components/portal/portal/portal.component';
import { UtilityBillFormComponent } from "./components/portal/utility-bill-form/utility-bill-form.component";
import { MeterReadingsComponent } from "./components/portal/meter-readings/meter-readings.component";
import { UserConsumersComponent } from "./components/portal/user-consumers/user-consumers.component";
import { ConsumerResolveService } from "./services/consumer.resolve.service";
import { ConsumersErrorComponent } from "./components/portal/consumers-error/consumers-error.component";
import { ConsumersResolveService } from "./services/consumers.resolve.service";
import { MetersReadingResolveService } from "./services/meters-reading.resolve.service";
import { UtilityBillsComponent } from "./components/portal/utility-bills/utility-bills.component";
import { DashboardComponent } from "./components/portal/dashboard/dashboard.component";
import { UtilityBillDetailsComponent } from "./components/portal/utility-bill-details/utility-bill-details.component";
import { UserProfileComponent } from "./components/user-profile/user-profile.component";
import { ChangePasswordComponent } from "./components/change-password/change-password.component";

export const portalRoutes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'utility-bill', resolve: { consumer: ConsumerResolveService }, component: UtilityBillFormComponent },
    { path: 'utility-bill/details/:id', resolve: { provider: ConsumerResolveService }, component: UtilityBillDetailsComponent },
    { path: 'utility-bills', resolve: { consumer: ConsumerResolveService }, component: UtilityBillsComponent },
    { path: 'meter-readings', resolve: { consumer: ConsumerResolveService }, component: MeterReadingsComponent },
    { path: 'user-consumers', component: UserConsumersComponent },
    { path: 'user-profile', component: UserProfileComponent },
    { path: "change-password", component: ChangePasswordComponent },
    { path: 'consumers-error', component: ConsumersErrorComponent }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        MatMenuModule,
        MatButtonModule,
        MatIconModule,
        MatInputModule,
        MatCardModule,
        MatRadioModule,
        MatSelectModule,
        MatSidenavModule,
        MatTableModule,
        MatToolbarModule,
        MatListModule
    ],
    declarations: [
        ConsumersErrorComponent,
        DashboardComponent,
        MeterReadingsComponent,
        PortalComponent,
        UserConsumersComponent,
        UtilityBillFormComponent,
        UtilityBillsComponent,
        UtilityBillDetailsComponent
    ],
    exports: [PortalComponent]
})
export class PortalModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: PortalModule,
            providers: [
                ConsumerResolveService,
                ConsumersResolveService,
                MetersReadingResolveService
            ]
        }
    }
}