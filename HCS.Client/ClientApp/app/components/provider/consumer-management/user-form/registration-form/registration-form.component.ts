import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { RegexService } from "../../../../../services/regex.service";
import { SettingsService } from "../../../../../services/settings.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { User } from "../../../../../models/user";
import { UserManagementService } from "../../../../../services/user-management.service";

@Component({
    selector: 'registration-form',
    templateUrl: './registration-form.component.html',
    styleUrls: ['./registration-form.component.css']
})
export class RegistrationFormComponent implements OnInit {
    
    @Input() user: User;
    @Output() onSubmit = new EventEmitter();
    isBlocked: boolean;

    constructor(private regexService: RegexService, private userManagementService: UserManagementService, private toastr: ToastsManager, private settingsService: SettingsService) { }
    
    ngOnInit() {
    }

    submit() {
        this.isBlocked = true;
        this.user.userName = this.user.email;
        if (!this.user.id) {
            this.userManagementService.create(this.user)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.user = data;
                    this.toastr.success('Ви створили користувача ' + this.user.userName, 'Успішно!');
                    this.onSubmit.emit(this.user);
                },
                err => {
                    this.isBlocked = false;
                    if (err.status == 400) {
                        this.toastr.error('Виникла помилка. Такий користувач вже зареєстрований.', 'Помилка!');
                    }
                    else {
                        this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    }
                    console.log(err);
                }
                );
        }
        else {
            this.userManagementService.update(this.user)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.user = data;
                    this.toastr.success('Ви оновили інформацію про користувача ' + this.user.userName, 'Успішно!');
                    this.onSubmit.emit(this.user);
                },
                err => {
                    this.isBlocked = false;
                    if (err.status == 404) {
                        this.toastr.error('Виникла помилка. Такий користувач не зареєстрований.', 'Помилка!');
                    }
                    else {
                        this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    }
                    console.log(err);
                }
                );
        }
        
    }
}