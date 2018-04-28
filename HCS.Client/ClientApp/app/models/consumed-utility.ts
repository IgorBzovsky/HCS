import { KeyValuePair } from "./key_value_pair";
import { Tariff } from "./tariff";

export interface SaveConsumedUtility {
    providedUtilityId: number;
    subsidy: number | null;
    consumption: number | null;
}

export interface ConsumedUtility {
    id: number;
    name: string;
    providedUtilityId: number;
    tariffId: number | null;
    subsidy: number | null;
    consumption: number | null;
    measureUnit: string;
    hasMeter: boolean;
    isSeasonal: boolean;
}

export interface ConsumedUtilityInfo {
    id: number;
    name: string;
    subsidy: number | null;
    consumption: number | null;
    measureUnit: string;
    hasMeter: boolean;
    isSeasonal: boolean;
    tariff: Tariff;
}