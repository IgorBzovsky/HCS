import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SharedModule } from './shared.module';

import { MatMenuModule, MatButtonModule, MatCheckboxModule, MatIconModule, MatCardModule, MatSidenavModule, MatTableModule, MatToolbarModule, MatListModule, MatInputModule } from '@angular/material';


import { AdminComponent } from './components/admin/admin/admin.component';
import { UserFormComponent } from "./components/admin/user-form/user-form.component";
import { UserListComponent } from "./components/admin/user-list/user-list.component";

import { UserManagementService } from "./services/user-management.service";
import { UserProfileComponent } from "./components/user-profile/user-profile.component";
import { ChangePasswordComponent } from "./components/change-password/change-password.component";


export const adminRoutes: Routes = [
    { path: '', redirectTo: 'user-list', pathMatch: 'full' },
    { path: 'user-form/new', component: UserFormComponent },
    { path: 'user-form/:id', component: UserFormComponent },
    { path: 'user-list', component: UserListComponent },
    { path: 'user-profile', component: UserProfileComponent },
    { path: "change-password", component: ChangePasswordComponent }
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
        MatMenuModule,
        MatSidenavModule,
        MatTableModule,
        MatToolbarModule,
        MatListModule,
        SharedModule
    ],
    declarations: [
        AdminComponent,
        UserFormComponent,
        UserListComponent
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