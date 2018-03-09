import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { AuthHttp } from 'angular2-jwt';
import { SettingsService } from "./settings.service";

@Injectable()
export class UserManagementService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    create(user: any) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/user-management', user).map(res => res.json());
    }
    getByName(userName: string) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/user-management/user-name/' + userName).map(res => res.json());
    }
    getRoles() {
        return [
            {
                id: 1,
                name: this.settings.RoleNames.admin
            },
            {
                id: 2,
                name: this.settings.RoleNames.provider
            },
            {
                id: 3,
                name: this.settings.RoleNames.consumer
            }
        ]
    }
}