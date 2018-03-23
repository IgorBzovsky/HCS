import { KeyValuePair } from "./key_value_pair";
import { DataSource } from "@angular/cdk/collections";
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/of";

export class Block {
    limit: number;
    price: number;
}

export class Tariff {
    id: number;
    name: string;
    subscriberFee: number;
    price: number;
    providedUtilityId: number;
    consumerTypeId: number;
    blocks: Block[];

    constructor() {
        this.blocks = [];
    }
}

export class TariffInfo {
    id: number;
    name: string;
    consumerType: string;
}

export class TariffListItem {
    id: number;
    position: number;
    name: string;
    providedUtility: string;
    consumerType: string;
    consumersQuantity: number;
}

export class TariffDataSource extends DataSource<any> {

    constructor(private tariffs: TariffListItem[]) {
        super();
    }

    connect(): Observable<TariffListItem[]> {
        return Observable.of(this.tariffs);
    }

    disconnect() { }
}