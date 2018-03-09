import { Injectable } from '@angular/core';

@Injectable()
export class SettingsService {
    private roleNames = {
        admin: 'Адміністратор',
        provider: 'Постачальник',
        consumer: 'Споживач'
    }

    private baseUrls = {
        webUrl: 'http://localhost:5000',
        apiUrl: 'http://localhost:5001',
        authUrl: 'http://localhost:5002'
    }

    get RoleNames() {
        return this.roleNames;
    }

    get BaseUrls() {
        return this.baseUrls;
    }
}