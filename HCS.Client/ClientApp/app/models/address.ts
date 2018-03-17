
import { KeyValuePair } from "./key_value_pair";

export class Address {
    id: number;
    region: KeyValuePair | null;
    district: KeyValuePair | null;
    locality: KeyValuePair | null;
    street: KeyValuePair | null;
    building: string;
    appartment: string;
}

export interface SaveAddress {
    id: number;
    parentId: number;
    building: string;
    appartment: string;
}

export interface LocationIncludeParent {
    id: number;
    name: string;
    parent: LocationIncludeParent;
    building: string;
    appartment: string;
}