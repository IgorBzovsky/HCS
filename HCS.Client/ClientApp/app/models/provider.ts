import { KeyValuePair } from "./key_value_pair";
import { ProvidedUtility } from "./provided-utility";

export class Provider {
    id: number;
    name: string;
    location: KeyValuePair | null;
    providedUtilities: ProvidedUtility[];
    constructor() {
        this.providedUtilities = [];
    }
}

export interface SaveProvider {
    id: number;
    name: string;
    locationId: number;
    providedUtilities: number[];
}