import { DataSource } from "@angular/cdk/collections";
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/of";

export class User {
    id: string;
    email: string;
    userName: string;
    password: string;
    confirmPassword: string;
    firstName: string;
    lastName: string;
    middleName: string;
    roles: string[];

    constructor() {
        this.roles = [];
    }
}

export class ChangePassword {
    currentPassword: string;
    newPassword: string;
    confirmPassword: string;
}

export class UserData {
    id: string;
    position: number;
    email: string;
    firstName: string;
    lastName: string;
    middleName: string;
    roles: string;

    constructor(user: User) {
        this.id = user.id;
        this.email = user.email;
        this.firstName = user.firstName;
        this.lastName = user.lastName;
        this.middleName = user.middleName;
        this.roles = user.roles.join(", ");
        if (!this.roles) {
            this.roles = "Зареєстрований користувач"
        }
    }
}

export class UserDataSource extends DataSource<any> {

    constructor(private user: UserData[]) {
        super();
    }

    connect(): Observable<UserData[]> {
        return Observable.of(this.user);
    }

    disconnect() { }
}