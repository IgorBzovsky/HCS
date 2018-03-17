import { Component, Input, OnInit } from "@angular/core";
import { Occupant } from "../../../../../../models/occupant";
import { Household } from "../../../../../../models/consumer";
import { RegexService } from "../../../../../../services/regex.service";
import { ConsumptionNorm } from "../../../../../../models/consumption-norm";

@Component({
    selector: "occupants-form",
    templateUrl: "./occupants-form.component.html"
})
export class OccupantsFormComponent implements OnInit {
    occupant = new Occupant();

    private _household: Household;
    get household(): Household {
        return this._household;
    }
    @Input()
    set household(household: Household) {
        this._household = household;
        this.refreshConsumptionNorms();
    }

    constructor(private regexService: RegexService) { }

    ngOnInit() {
    }

    submit() {

    }

    private refreshConsumptionNorms() {
        this.occupant.consumptionNorms = [];
        for (let i = 0; i < this.household.consumedUtilities.length; i++) {
            this.occupant.consumptionNorms.push(new ConsumptionNorm(this.household.consumedUtilities[i].id, this.household.consumedUtilities[i].name));
        }
    }
}