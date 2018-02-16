import { Injectable } from '@angular/core';

@Injectable()
export class SettingsService {
    roleNames = {
        admin: 'Адміністратор системи',
        provider: 'Постачальник',
        consumer: 'Споживач'
    }

    baseUrls = {
        webUrl: 'http://localhost:5000',
        apiUrl: 'http://localhost:5001',
        authUrl: 'http://localhost:5002'
    }
}