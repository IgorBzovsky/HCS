import { Injectable } from '@angular/core';
import { AuthHttp } from 'angular2-jwt';
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';
import { Tariff } from "../models/tariff";

@Injectable()
export class TariffService {
    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }
    create(tariff: Tariff) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/tariffs', tariff).map(res => res.json());
    }

    update(tariff: Tariff) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/tariffs/' + tariff.id, tariff).map(res => res.json());
    }

    getById(id: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/tariffs/' + id).map(res => res.json());
    }

    getAllByProviderId(providerId: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/tariffs/provider/' + providerId).map(res => res.json());
    }

    delete(id: number) {
        return this.authHttp.delete(this.settings.BaseUrls.apiUrl + '/tariffs/' + id).map(res => res.json());
    }
}