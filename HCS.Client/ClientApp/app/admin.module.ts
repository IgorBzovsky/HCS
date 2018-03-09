import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { MatMenuModule, MatButtonModule, MatCheckboxModule, MatIconModule, MatCardModule, MatSidenavModule, MatToolbarModule, MatListModule, MatInputModule } from '@angular/material';


import { AdminComponent } from './components/admin/admin/admin.component';
import { MainComponent } from "./components/admin/main/main.component";
import { MoreInfoComponent } from "./components/admin/more-info/more-info.component";
import { NavMenuComponent } from "./components/admin/navmenu/navmenu.component";
import { UserFormComponent } from "./components/admin/user-form/user-form.component";

import { UserManagementService } from "./services/user-management.service";

export const adminRoutes: Routes = [
    { path: '', redirectTo: 'main', pathMatch: 'full' },
    { path: 'main', component: MainComponent },
    { path: 'more-info', component: MoreInfoComponent }
];


@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        MatMenuModule,
        MatButtonModule,
        MatCheckboxModule,
        MatInputModule,
        MatIconModule,
        MatCardModule,
        MatSidenavModule,
        MatToolbarModule,
        MatListModule
    ],
    declarations: [
        AdminComponent,
        MainComponent,
        MoreInfoComponent,
        NavMenuComponent,
        UserFormComponent
    ],
    exports: [AdminComponent]
})
export class AdminModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: AdminModule,
            providers: [
                UserManagementService
            ]
        }
    }
}