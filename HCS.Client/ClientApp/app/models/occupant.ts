import { Exemption } from "./exemption";
import { ConsumptionNorm } from "./consumption-norm";

export class Occupant {
    id: number;
    firstName: string;
    lastName: string;
    middleName: string;
    householdId: number;
    exemption: Exemption;
    consumptionNorms: ConsumptionNorm[];

    constructor() {
        this.consumptionNorms = [];
    }
}