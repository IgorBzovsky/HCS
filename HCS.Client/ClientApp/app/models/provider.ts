import { KeyValuePair } from "./key_value_pair";

export interface Provider {
    id: number;
    name: string;
    location: KeyValuePair;
    providedUtilities: KeyValuePair[];
}

export interface SaveProvider {
    id: number;
    name: string;
    locationId: number;
    providedUtilities: number[];
}