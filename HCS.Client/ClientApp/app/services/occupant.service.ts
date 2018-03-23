import { AuthHttp } from 'angular2-jwt';
import { Injectable } from '@angular/core';
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';
import { Occupant } from "../models/occupant";


@Injectable()
export class OccupantService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }

    create(occupant: Occupant) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/occupants', occupant).map(res => res.json());
    }

    update(occupant: Occupant) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/occupants/' + occupant.id, occupant).map(res => res.json());
    }

    delete(id: number) {
        return this.authHttp.delete(this.settings.BaseUrls.apiUrl + '/occupants/' + id).map(res => res.json());
    }

    get(id: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/occupants/' + id).map(res => res.json());
    }

    getByHousehold(householdId: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/occupants/household/' + householdId).map(res => res.json());
    }
}