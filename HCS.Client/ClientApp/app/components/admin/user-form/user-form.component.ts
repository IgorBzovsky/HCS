import { Component, OnInit } from '@angular/core';
import { UserManagementService } from "../../../services/user-management.service";
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import { RegexService } from "../../../services/regex.service";
import { User } from "../../../models/user";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'user-form',
    templateUrl: './user-form.component.html',
    styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
    roles: any[];
    isBlocked: boolean;
    user = new User();

    constructor(private userManagementService: UserManagementService, private regexService: RegexService, private toastr: ToastsManager, private route: ActivatedRoute, private router: Router) {
        route.params.subscribe(p => {
            this.user.id = p['id'];
        });
    }

    ngOnInit() {
        this.userManagementService.getById(this.user.id)
            .subscribe(user => {
                this.user = user;
            });
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
        if (this.user.id) {
            this.userManagementService.update(this.user)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.router.navigate(["admin/user-list"]);
                    this.toastr.success('Ви оновили інформацію користувача ' + this.user.userName, 'Успішно!');
                },
                err => {
                    this.isBlocked = false;
                    if (err.status == 404) {
                        this.toastr.error('Такого користувача не існує.', 'Помилка!');
                    }
                    else {
                        this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    }
                }
                );
        }
        else {
            this.userManagementService.create(this.user)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.router.navigate(["admin/user-list"]);
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
}