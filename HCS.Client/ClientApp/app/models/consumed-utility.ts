import { KeyValuePair } from "./key_value_pair";

export interface SaveConsumedUtility {
    providedUtilityId: number;
    obligatoryPrice: number | null;
}

export interface ConsumedUtility {
    id: number;
    providedUtilityId: number;
    name: string;
    obligatoryPrice: number | null;
}