import { Component, OnInit } from "@angular/core";
import { User, UserData, UserDataSource } from "../../../models/user";
import { UserManagementService } from "../../../services/user-management.service";
import { Router } from "@angular/router";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { AuthService } from "../../../services/auth.service";


@Component({
    selector: "user-list",
    templateUrl: "./user-list.component.html",
    styleUrls: ["./user-list.component.css"]
})
export class UserListComponent implements OnInit {
    users: User[] = [];
    usersData: UserData[] = [];
    displayedColumns = ['email', 'lastName', 'firstName', 'middleName', 'roles', 'delete'];
    dataSource: UserDataSource;

    constructor(private userManagementService: UserManagementService, private router: Router, private toastr: ToastsManager, private authService: AuthService) { }

    ngOnInit() {
        this.userManagementService.get()
            .subscribe(
            data => {
                this.users = data;
                this.populateTable();
                this.dataSource = new UserDataSource(this.usersData);
            },
            err => {
                console.log("Error (get user list)", err);
            })
        
    }

    delete(userData: UserData) {
        if (confirm("Ви впевнені, що хочете видалити користувача?")) {
            this.userManagementService.delete(userData.id)
                .subscribe(
                data => {
                    const index = this.usersData.map(x => x.id).indexOf(userData.id);
                    if (index != -1) {
                        this.usersData.splice(index, 1);
                    }
                    this.dataSource = new UserDataSource(this.usersData);
                    this.toastr.success('Ви видалили користувача ' + userData.email, 'Успішно!');
                },
                err => {
                    if (err.status == 404) {
                        this.toastr.error('Такого користувача не існує.', 'Помилка!');
                    }
                    else {
                        this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    }
                }
                );
        }
    }

    edit(id: string) {
        this.router.navigate(["admin/user-form/" + id]);
    }

    private populateTable() {
        for (let i = 0; i < this.users.length; i++) {
            let userData = new UserData(this.users[i]);
            userData.position = i + 1;
            this.usersData.push(userData);
        }
    }
}