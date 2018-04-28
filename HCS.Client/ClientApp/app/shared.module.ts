import { NgModule } from "@angular/core";
import { EqualValidator } from "./directives/equal-validator.directive";
import { UserProfileComponent } from "./components/user-profile/user-profile.component";
import { ChangePasswordComponent } from "./components/change-password/change-password.component";
import { MatButtonModule, MatCardModule, MatCheckboxModule, MatIconModule, MatInputModule } from "@angular/material";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        MatButtonModule,
        MatCardModule,
        MatCheckboxModule,
        MatIconModule,
        MatInputModule
    ],
    declarations: [
        EqualValidator,
        ChangePasswordComponent,
        UserProfileComponent
],
    exports: [
        EqualValidator,
        ChangePasswordComponent,
        UserProfileComponent
    ]
})
export class SharedModule { }