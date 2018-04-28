import { Component } from "@angular/core";
import { UserManagementService } from "../../services/user-management.service";
import { RegexService } from "../../services/regex.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Router } from "@angular/router";
import { User } from "../../models/user";
import { AuthService } from "../../services/auth.service";

@Component({
    selector: "user-profile",
    templateUrl: "./user-profile.component.html"
})
export class UserProfileComponent {
    isBlocked: boolean;
    user = new User();

    constructor(private userManagementService: UserManagementService, private regexService: RegexService, private toastr: ToastsManager, private router: Router, private authService: AuthService) {
    }

    ngOnInit() {
        this.userManagementService.getByName(this.authService.getProfile().email)
            .subscribe(user => {
                this.user = user;
                console.log(this.user);
            });
    }

    submit() {
        this.isBlocked = true;
        this.user.userName = this.user.email;
        this.userManagementService.update(this.user)
            .subscribe(
            data => {
                this.isBlocked = false;
                this.user = data;
                this.toastr.success('Ви оновили інформацію користувача ' + this.user.userName, 'Успішно!');
                this.router.navigate(["admin"]);
            },
            err => {
                this.isBlocked = false;
                this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
            });
    }
}