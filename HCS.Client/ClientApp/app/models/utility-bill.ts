import { ConsumedUtilityInfo, ConsumedUtility } from "./consumed-utility";
import { DataSource } from "@angular/cdk/collections";
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/of";

export class SaveUtilityBill {
    id: number;
    month: number;
    year: number;
    isMetersReading: boolean;
    consumerId: number;
    utilityBillLines: SaveUtilityBillLine[];

    constructor() {
        this.utilityBillLines = [];
    }
}

export class UtilityBill {
    id: number;
    month: number | null;
    year: number | null;
    isMetersReading: boolean;
    consumerId: number;
    utilityBillLines: UtilityBillLine[];

    constructor() {
        this.utilityBillLines = [];
    }
}

export class UtilityBillListItem {
    id: number;
    month: number | null;
    year: number | null;
    total: number
}

export class UtilityBillData {
    id: number;
    position: number;
    month: number | null;
    year: number | null;
    total: number

    constructor(utilityBill: UtilityBillListItem) {
        this.id = utilityBill.id;
        this.month = utilityBill.month;
        this.year = utilityBill.year;
        this.total = utilityBill.total;
    }
}

export class SaveUtilityBillLine {
    id: number;
    consumedUtilityId: number; 
    meterReadingEnd: number;
}

export class UtilityBillLine {
    id: number;
    consumedUtility: ConsumedUtility;
    meterReadingStart: number;
    meterReadingEnd: number;
    price: number;
}

export class UtilityBillLineData {
    id: number;
    position: number;
    consumedUtility: string;
    meterReadingStart: number | null;
    meterReadingEnd: number | null;
    consumption: number | null;
    price: number;

    constructor(utilityBillLine: UtilityBillLine) {
        this.id = utilityBillLine.id;
        this.consumedUtility = utilityBillLine.consumedUtility.name;
        if (utilityBillLine.consumedUtility.hasMeter) {
            this.meterReadingStart = utilityBillLine.meterReadingStart;
            this.meterReadingEnd = utilityBillLine.meterReadingEnd;
            this.consumption = null;
        }
        else {
            this.meterReadingStart = null;
            this.meterReadingEnd = null;
            this.consumption = utilityBillLine.consumedUtility.consumption;
        }
        this.price = utilityBillLine.price;
    }
}

export class UtilityBillLinesDataSource extends DataSource<any> {

    constructor(private utilityBillLines: UtilityBillLineData[]) {
        super();
    }

    connect(): Observable<UtilityBillLineData[]> {
        return Observable.of(this.utilityBillLines);
    }

    disconnect() { }
}

export class UtilityBillsDataSource extends DataSource<any> {

    constructor(private utilityBills: UtilityBillData[]) {
        super();
    }

    connect(): Observable<UtilityBillData[]> {
        return Observable.of(this.utilityBills);
    }

    disconnect() { }
}