import { Injectable } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';

@Injectable()
export class ProviderService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    getUtilities() {
        return this.authHttp.get(this.settings.baseUrls.apiUrl + '/utilities').map(res => res.json());
    }
    getRegions() {
        return this.authHttp.get(this.settings.baseUrls.apiUrl + '/regions').map(res => res.json());
    }
    create(provider: any) {
        return this.authHttp.post(this.settings.baseUrls.apiUrl + '/providers', provider).map(res => res.json());
    }
}