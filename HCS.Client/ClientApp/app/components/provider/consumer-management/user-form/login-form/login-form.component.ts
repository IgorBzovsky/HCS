import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { SettingsService } from "../../../../../services/settings.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { User } from "../../../../../models/user";
import { UserManagementService } from "../../../../../services/user-management.service";


@Component({
    selector: 'login-form',
    templateUrl: './login-form.component.html'
})
export class LoginFormComponent implements OnInit {

    @Input() user: User;
    @Output() onSubmit = new EventEmitter();
    isBlocked: boolean;

    constructor(private userManagementService: UserManagementService, private toastr: ToastsManager, private settingsService: SettingsService) { }

    ngOnInit() {

    }

    submit() {
        this.isBlocked = true;
        this.user.userName = this.user.email;
        this.userManagementService.getByName(this.user.userName)
            .subscribe(
            data => {
                this.isBlocked = false;
                this.user = data;
                this.onSubmit.emit(this.user);
            },
            err => {
                this.isBlocked = false;
                if (err.status == 404) {
                    this.toastr.error('Виникла помилка. Користувач не зареєстрований.', 'Помилка!');
                }
                else {
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                }
                console.log(err);
            }
            );
    }

}