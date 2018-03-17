import { KeyValuePair } from "./key_value_pair";
import { ConsumedUtility } from "./consumed-utility";
import { Address, LocationIncludeParent } from "./address";
import { DataSource } from "@angular/cdk/collections";
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/of";

export class Household {
    id: number;
    area: number | null;
    isDraft: boolean;
    hasElectricHeating: boolean;
    hasTowelRail: boolean;
    hasElectricHotplates: boolean;
    hasCentralGasSupply: boolean;
    hasSubsidy: boolean;
    applicationUserId: string;
    locationId: number;
    consumerType: KeyValuePair;
    consumerCategory: KeyValuePair;
    consumedUtilities: ConsumedUtility[];

    constructor() {
        this.consumedUtilities = [];
        this.consumerCategory = new KeyValuePair();
        this.consumerType = new KeyValuePair();
    }
}

export class Organization {
    id: number;
    isDraft: boolean;
    organizationName: string;
    applicationUserId: string;
    locationId: number;
    consumerType: KeyValuePair;
    consumerCategory: KeyValuePair;
    consumedUtilities: ConsumedUtility[];

    constructor() {
        this.consumedUtilities = [];
        this.consumerCategory = new KeyValuePair();
        this.consumerType = new KeyValuePair();
    }
}

export class ConsumerLocation {
    id: number;
    consumerType: KeyValuePair;
    applicationUserId: string;
    location: LocationIncludeParent;

    constructor() {
        this.consumerType = new KeyValuePair();
    }
}

export class ConsumerLocationData {
    id: number;
    position: number;
    region: string;
    district: string;
    locality: string;
    street: string;
    building: string;
    appartment: string;
    consumerType: string;

    constructor(consumer: ConsumerLocation) {
        this.id = consumer.id;
        this.building = consumer.location.building;
        this.appartment = consumer.location.appartment;
        let street = consumer.location.parent;
        this.street = street.name;
        let locality = street.parent;
        this.locality = locality.name;
        let district = locality.parent;
        this.district = district.name;
        let region = district.parent;
        this.region = region.name;
        this.consumerType = consumer.consumerType.name;
    }
}

export class ConsumerDataSource extends DataSource<any> {

    constructor(private consumer: ConsumerLocationData[]) {
        super();
    }

    connect(): Observable<ConsumerLocationData[]> {
        return Observable.of(this.consumer);
    }

    disconnect() { }
}