import { Injectable } from '@angular/core';

declare var HcsAppSettings: any;

export interface IHcsAppSettings {
    BaseUrls: IBaseUrls,
    LogoutRedirect: string,
    AccessToken: string,
    UserFullName: string,
    UserEmail: string,
    AdminEnabled: boolean
}

export interface IBaseUrls {
    Api: string,
    Auth: string,
    Web: string
}

@Injectable()
export class AppSettingsService {
    public appSettings: IHcsAppSettings;

    constructor() {
        this.loadSettings();
    }

    loadSettings() {
        this.appSettings = {
            BaseUrls: {
                Api: HcsAppSettings.BaseUrls.Api,
                Auth: HcsAppSettings.BaseUrls.Auth,
                Web: HcsAppSettings.BaseUrls.Web
            },
            LogoutRedirect: HcsAppSettings.LogoutRedirect,
            AccessToken: HcsAppSettings.AccessToken,
            UserFullName: HcsAppSettings.UserFullName,
            UserEmail: HcsAppSettings.UserEmail,
            AdminEnabled: HcsAppSettings.AdminEnabled
        };
    }
}