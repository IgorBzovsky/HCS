import { AuthHttp } from 'angular2-jwt';
import { Injectable } from '@angular/core';
import { Household } from "../models/consumer";
import { SettingsService } from "./settings.service";
import 'rxjs/add/operator/map';



@Injectable()
export class ConsumerService {

    private _months = ["Січень", "Лютий", "Березень", "Квітень", "Травень", "Червень", "Липень", "Серпень", "Вересень", "Жовтень", "Листопад", "Грудень"];

    private _currentConsumerId = 0;

    constructor(private authHttp: AuthHttp, private settings: SettingsService) { }

    get currentConsumerId(){
        return this._currentConsumerId;
    }

    set currentConsumerId(id: number) {
        this._currentConsumerId = id;
    }

    get months() {
        return this._months;
    }

    getMonth(month: number) {
        if (month < 1 || month > 12)
            return "";
        return this._months[month - 1];
    }

    
    getCurrentConsumer() {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/consumer-info/' + this.currentConsumerId).map(res => res.json());
    }

    getMetersReading(consumerId: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/utility-bills/consumer/' + consumerId + '/meters-reading').map(res => res.json());
    }

    getUtilityBills(consumerId: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/utility-bills/consumer/' + consumerId).map(res => res.json());
    }

    getUtilityBill(utilityBillId: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/utility-bills/' + utilityBillId).map(res => res.json());
    }

    create(consumer: any) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/consumers', consumer).map(res => res.json());
    }

    createMetersReading(metersReading: any) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/utility-bills/meters-reading', metersReading).map(res => res.json());
    }

    createUtilityBill(utilityBill: any) {
        return this.authHttp.post(this.settings.BaseUrls.apiUrl + '/utility-bills', utilityBill).map(res => res.json());
    }

    update(consumer: any) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/consumers/' + consumer.id, consumer).map(res => res.json());
    }

    updateMetersReading(metersReading: any) {
        return this.authHttp.put(this.settings.BaseUrls.apiUrl + '/utility-bills/meters-reading/' + metersReading.id, metersReading).map(res => res.json());
    }

    getAll(providerId: number) {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/provider/' + providerId).map(res => res.json());
    }

    getUserConsumers() {
        return this.authHttp.get(this.settings.BaseUrls.apiUrl + '/consumers/user/current').map(res => res.json());
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