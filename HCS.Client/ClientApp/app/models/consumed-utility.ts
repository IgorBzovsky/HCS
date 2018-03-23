import { KeyValuePair } from "./key_value_pair";

export interface SaveConsumedUtility {
    providedUtilityId: number;
    obligatoryPrice: number | null;
}

export interface ConsumedUtility {
    id: number;
    name: string;
    providedUtilityId: number;
    tariffId: number | null;
    obligatoryPrice: number | null;
    measureUnit: string;
    hasMeter: boolean;
    isSeasonal: boolean;
}