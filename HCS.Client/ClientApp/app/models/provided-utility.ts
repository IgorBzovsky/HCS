import { TariffInfo } from "./tariff";

export class ProvidedUtility {
    id: number;
    utilityId: number;
    name: string;
    measureUnit: string;
    isSeasonal: boolean;
    tariffs: TariffInfo[];

    constructor() {
        this.tariffs = [];
    }
}