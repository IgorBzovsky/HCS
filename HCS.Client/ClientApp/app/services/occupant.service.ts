import { AuthHttp } from 'angular2-jwt';
import { Injectable } from '@angular/core';
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';
import { Occupant } from "../models/occupant";


@Injectable()
export class LocationService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }

    create(occupant: Occupant) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/occupants', occupant).map(res => res.json());
    }
}