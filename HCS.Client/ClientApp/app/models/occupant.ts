import { Exemption } from "./exemption";
import { ConsumptionNorm } from "./consumption-norm";

export class Occupant {
    id: number;
    firstName: string;
    lastName: string;
    middleName: string;
    consumerId: number;
    exemptionId: number;
    consumptionNorms: ConsumptionNorm[];

    constructor() {
        this.consumptionNorms = [];
    }
}