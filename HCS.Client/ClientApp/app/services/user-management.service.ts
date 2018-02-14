import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { AuthHttp } from 'angular2-jwt';

@Injectable()
export class UserManagementService {
    constructor(private authHttp: AuthHttp) { }
    create(user: any) {
        console.log(user);
        return this.authHttp.post('http://localhost:5001/api/user-management', user).map(res => res.json());
    }
    getRoles() {
        return [
            {
                id: 1,
                name: "Адміністратор"
            },
            {
                id: 2,
                name: "Служба соц. забезпечення"
            },
            {
                id: 3,
                name: "Постачальник"
            }
        ]
    }
}