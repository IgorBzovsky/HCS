import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { AuthHttp } from 'angular2-jwt';
import { SettingsService } from "./settings.service";

@Injectable()
export class UserManagementService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    create(user: any) {
        return this.authHttp.post(this.settings.baseUrls.apiUrl + '/user-management', user).map(res => res.json());
    }
    getRoles() {
        return [
            {
                id: 1,
                name: this.settings.roleNames.admin
            },
            {
                id: 2,
                name: this.settings.roleNames.provider
            },
            {
                id: 3,
                name: this.settings.roleNames.consumer
            }
        ]
    }
}