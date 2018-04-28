import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { AuthHttp } from 'angular2-jwt';
import { SettingsService } from "./settings.service";
import { User, ChangePassword } from "../models/user";
import { HttpParams } from '@angular/common/http';

@Injectable()
export class UserManagementService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    create(user: User) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/user-management', user).map(res => res.json());
    }
    update(user: User) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/user-management/' + user.id, user).map(res => res.json());
    }
    get(userQuery: any) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/user-management?' + this.toQueryString(userQuery)).map(res => res.json());
    }
    getById(id: string) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/user-management/' + id).map(res => res.json());
    }
    getByName(userName: string) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/user-management/user-name/' + userName).map(res => res.json());
    }
    delete(id: string) {
        return this.authHttp.delete(this.settings.BaseUrls.apiUrl + '/user-management/' + id).map(res => res.json());
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
            }
        ]
    }
    changePassword(changePassword: ChangePassword) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/user-management/user/password', changePassword).map(res => res.json());
    }

    private toQueryString(obj: any) {
        var parts = [];
        for (let property in obj) {
            let value = obj[property];
            if (value != null && value != undefined) {
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
            }
        }
        return parts.join('&');
    }
}