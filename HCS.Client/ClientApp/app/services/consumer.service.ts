import { AuthHttp } from 'angular2-jwt';
import { Injectable } from '@angular/core';
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';
import { Household } from "../models/consumer";


@Injectable()
export class ConsumerService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }

    createHousehold(household: Household) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/consumers/household', household).map(res => res.json());
    }

    updateHousehold(household: Household) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/consumers/household/' + household.id, household).map(res => res.json());
    }

    getConsumerByLocationId(locationId: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/location/' + locationId).map(res => res.json());
    }
}