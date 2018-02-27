import { Injectable } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';

@Injectable()
export class LocationService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    
    getRegions() {
        return this.authHttp.get(this.settings.baseUrls.apiUrl + '/regions').map(res => res.json());
    }
}