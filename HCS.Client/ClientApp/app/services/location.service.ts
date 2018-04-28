﻿import { AuthHttp } from 'angular2-jwt';
import { Injectable } from '@angular/core';
import { SettingsService } from "./settings.service";
import { SaveAddress, LocationIncludeParent, Address } from "../models/address";
import 'rxjs/add/operator/map';
import { KeyValuePair } from "../models/key_value_pair";


@Injectable()
export class LocationService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    
    getRegions() {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/locations/regions').map(res => res.json());
    }

    getLocationByParentId(id: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/locations/parent/' + id).map(res => res.json())
    }

    get(id: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/locations/' + id).map(res => res.json())
    }

    create(address: SaveAddress) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/locations', address).map(res => res.json());
    }

    update(address: SaveAddress) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/locations/' + address.id, address).map(res => res.json());
    }

    stringifyUtilityBillAddress(address: Address) {
        let billAddress = "";
        if (address.street)
            billAddress += "вул. " + address.street.name + " ";
        billAddress += address.building;
        if (address.appartment) {
            billAddress += "/" + address.appartment;
        }
        if (address.locality)
            billAddress += " м." + address.locality.name;
        return billAddress;
    }

    mapFromLocation(location: LocationIncludeParent) {
        let address = new Address;
        address.id = location.id;
        address.building = location.building;
        address.appartment = location.appartment;
        let street = location.parent;
        address.street = new KeyValuePair(street.id, street.name);
        let locality = street.parent;
        address.locality = new KeyValuePair(locality.id, locality.name);
        let district = locality.parent;
        address.district = new KeyValuePair(district.id, district.name);
        let region = district.parent;
        address.region = new KeyValuePair(region.id, region.name);
        return address;
    }
}