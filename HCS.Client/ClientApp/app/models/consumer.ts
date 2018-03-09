export interface Household {
    id: number;
    area: number | null;
    hasElectricHeating: boolean;
    hasTowelRail: boolean;
    hasElectricHotplates: boolean;
    hasCentralGasSupply: boolean;
    applicationUserId: string;
    locationId: number;
}