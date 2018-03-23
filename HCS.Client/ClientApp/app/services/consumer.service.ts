import { AuthHttp } from 'angular2-jwt';
import { Injectable } from '@angular/core';
import { Household } from "../models/consumer";
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';



@Injectable()
export class ConsumerService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }

    create(consumer: any) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/consumers', consumer).map(res => res.json());
    }

    update(consumer: any) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/consumers/' + consumer.id, consumer).map(res => res.json());
    }

    getAll() {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers').map(res => res.json());
    }

    getTypes() {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/types').map(res => res.json());
    }

    getCategoriesByTypeName(name: string) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/categories/type/' + name).map(res => res.json());
    }

    getConsumerByLocationId(locationId: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/location/' + locationId).map(res => res.json());
    }

    getExemptions() {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/exemptions').map(res => res.json())
    }

    get(id: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/' + id).map(res => res.json());
    }
}