import { AuthHttp } from 'angular2-jwt';
import { Injectable } from '@angular/core';
import { SettingsService } from "./settings.service";
import { SaveAddress } from "../models/address";
import 'rxjs/add/operator/map';


@Injectable()
export class LocationService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    
    getRegions() {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/locations/regions').map(res => res.json());
    }

    getLocationByParentId(id: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/locations/parent/' + id).map(res => res.json())
    }

    create(address: SaveAddress) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/locations', address).map(res => res.json());
    }

    update(address: SaveAddress) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/locations/' + address.id, address).map(res => res.json());
    }
}