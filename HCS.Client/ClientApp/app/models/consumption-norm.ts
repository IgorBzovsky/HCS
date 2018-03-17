export class ConsumptionNorm {
    id: number;
    amount: number;
    consumedUtilityId: number;
    utilityName: string;

    constructor(consumedUtilityId: number, utilityName: string) {
        this.consumedUtilityId = consumedUtilityId;
        this.utilityName = utilityName;
    }
}