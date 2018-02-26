﻿import { Injectable } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';
import { SaveProvider } from "../models/provider";

@Injectable()
export class ProviderService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    getUtilities() {
        return this.authHttp.get(this.settings.baseUrls.apiUrl + '/utilities').map(res => res.json());
    }
    getRegions() {
        return this.authHttp.get(this.settings.baseUrls.apiUrl + '/regions').map(res => res.json());
    }
    create(provider: SaveProvider) {
        return this.authHttp.post(this.settings.baseUrls.apiUrl + '/providers', provider).map(res => res.json());
    }

    update(provider: SaveProvider) {
        return this.authHttp.put(this.settings.baseUrls.apiUrl + '/providers/' + provider.id, provider).map(res => res.json());
    }

    getProvider() {
        return this.authHttp.get(this.settings.baseUrls.apiUrl + '/providers/current').map(res => res.json())
    }
}