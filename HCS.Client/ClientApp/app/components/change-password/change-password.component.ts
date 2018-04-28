import { Component } from "@angular/core";
import { ChangePassword } from "../../models/user";
import { RegexService } from "../../services/regex.service";
import { UserManagementService } from "../../services/user-management.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Router } from "@angular/router";

@Component({
    selector: "change-password",
    templateUrl: "./change-password.component.html"
})
export class ChangePasswordComponent {

    isBlocked: boolean;
    changePassword = new ChangePassword;

    constructor(private regexService: RegexService, private userManagementService: UserManagementService, private toastr: ToastsManager, private router: Router) { }

    submit() {
        this.isBlocked = true;
        this.userManagementService.changePassword(this.changePassword)
            .subscribe(data => {
                this.isBlocked = false;
                this.toastr.success("Ви змінили пароль", "Успішно!");
                this.router.navigate(["admin"]);
            },
            err => {
                console.log(err);
                this.isBlocked = false;
                this.toastr.error("Не вдалося змінити пароль", "Помилка!");
            });
    }
}