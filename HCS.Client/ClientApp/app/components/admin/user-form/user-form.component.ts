﻿import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { UserManagementService } from "../../../services/user-management.service";
import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
    selector: 'user-form',
    templateUrl: './user-form.component.html',
    styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
    roles: any;
    isBlocked: boolean;
    user: any = {
        roles: []
    };

    constructor(private userManagementService: UserManagementService, private toastr: ToastsManager, vcr: ViewContainerRef) {
        this.toastr.setRootViewContainerRef(vcr);
    }

    ngOnInit() {
        this.roles = this.userManagementService.getRoles();
    }

    onRoleToggle(roleName: string, $event: any) {
        if ($event.checked)
            this.user.roles.push(roleName);
        else {
            var index = this.user.roles.indexOf(roleName);
            this.user.roles.splice(index, 1);
        }
    }

    submit() {
        this.isBlocked = true;
        this.user.userName = this.user.email;
        this.userManagementService.create(this.user)
            .subscribe(
            x => {
                this.isBlocked = false;
                this.toastr.success('Ви створили користувача ' + this.user.userName, 'Успішно!');
            },
            err => {
                this.isBlocked = false;
                this.toastr.error('Виникла помилка. Можливо користувач вже зареєстрований.', 'Помилка!');
                console.log(err);
            }
        );
    }
}