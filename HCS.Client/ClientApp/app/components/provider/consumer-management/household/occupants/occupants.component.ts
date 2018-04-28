import * as _ from 'underscore';
import { Component, Input } from "@angular/core";
import { Household } from "../../../../../models/consumer";
import { Occupant } from "../../../../../models/occupant";
import { Exemption } from "../../../../../models/exemption";
import { OccupantService } from "../../../../../services/occupant.service";
import { ConsumedUtility } from "../../../../../models/consumed-utility";
import { ProvidedUtility } from "../../../../../models/provided-utility";

@Component({
    selector: "occupants",
    templateUrl: "./occupants.component.html"
})
export class OccupantsComponent {
    @Input() exemptions: Exemption[];
    @Input() providedUtilities: ProvidedUtility[];

    private _household: Household;
    get household(): Household {
        return this._household;
    }
    @Input()
    set household(household: Household) {
        console.log("Set household");
        this._household = household;
        if (household.id) {
            this.occupantService.getByHousehold(household.id)
                .subscribe(data => {
                    this.occupants = data
                });
        }
        this.occupant = new Occupant();
        this.initConsumptionNorms();
    }

    private addConsumptionNorms() {
        for (let i = 0; i < this.household.consumedUtilities.length; i++) {
            let utility = this.household.consumedUtilities[i];
            if (!this.occupant.consumptionNorms.find(x => x.consumedUtilityId == utility.id)) {
                this.addEmptyNorm(this.occupant, utility);
            }
        }
    }

    selectedTab = 0;
    occupants: Occupant[] = [];
    occupant = new Occupant();

    constructor(private occupantService: OccupantService) { }

    occupantsFormSubmit($event: Occupant) {
        this.occupantService.getByHousehold(this.household.id)
            .subscribe(data => {
                this.occupants = data,
                this.selectedTab = 0,
                this.occupant = new Occupant(),
                this.initConsumptionNorms();
            });
    }

    editOccupant($event: number) {
        this.occupantService.get($event)
            .subscribe(occupant => {
                this.setOccupant(occupant),
                this.addConsumptionNorms(),
                this.selectedTab = 1
            },
            err => {
                console.log(err);
            });
    }

    createOccupant() {
        this.occupant = new Occupant();
        this.initConsumptionNorms();
        this.selectedTab = 1;
    }

    private initConsumptionNorms() {
        this.occupant.consumptionNorms = [];
        for (let i = 0; i < this.household.consumedUtilities.length; i++) {
            let utility = this.household.consumedUtilities[i];
            this.addEmptyNorm(this.occupant, utility);
        }
    }

    private addEmptyNorm(occupant: Occupant, utility: ConsumedUtility) {
        occupant.consumptionNorms.push({
            consumedUtilityId: utility.id,
            isSeasonal: false,
            utilityName: utility.name,
            measureUnit: utility.measureUnit,
            amount: 0,
            id: 0
        });
        if (utility.isSeasonal) {
            occupant.consumptionNorms.push({
                consumedUtilityId: utility.id,
                isSeasonal: true,
                utilityName: utility.name,
                measureUnit: utility.measureUnit,
                amount: 0,
                id: 0
            });
        }
    }

    private selectTab($event: any) {
        console.log($event);
    }

    private setOccupant(occupant: any) {
        console.log(occupant);
        this.occupant = {
            id: occupant.id,
            consumerId: this.household.id,
            lastName: occupant.lastName,
            firstName: occupant.firstName,
            middleName: occupant.middleName,
            exemptionId: occupant.exemption.id,
            consumptionNorms: occupant.consumptionNorms
        }
        console.log(this.occupant);
    }
}