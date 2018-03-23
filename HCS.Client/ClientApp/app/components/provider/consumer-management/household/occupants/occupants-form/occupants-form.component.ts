import { Component, Input, Output, EventEmitter, OnInit } from "@angular/core";
import { Occupant } from "../../../../../../models/occupant";
import { Household } from "../../../../../../models/consumer";
import { RegexService } from "../../../../../../services/regex.service";
import { ConsumptionNorm } from "../../../../../../models/consumption-norm";
import { OccupantService } from "../../../../../../services/occupant.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { ConsumedUtility } from "../../../../../../models/consumed-utility";
import { Exemption } from "../../../../../../models/exemption";

@Component({
    selector: "occupants-form",
    templateUrl: "./occupants-form.component.html"
})
export class OccupantsFormComponent implements OnInit {

    @Input() occupant: Occupant;
    @Input() household: Household;
    @Input() exemptions: Exemption[];
    
    @Output() onSubmit = new EventEmitter();

    constructor(private regexService: RegexService, private occupantService: OccupantService, private toastr: ToastsManager) { }

    ngOnInit() {
    }

    submit() {
        if (!this.occupant.id) {
            this.occupant.consumerId = this.household.id;
            this.occupantService.create(this.occupant)
                .subscribe(data => {
                    this.occupant = data;
                    this.toastr.success("Ви створили мешканця", "Успішно!");
                    this.onSubmit.emit(this.occupant);
                },
                err => {
                    this.toastr.error("Виникла невідома помилка на сервері", "Помилка");
                })
        }
        else {
            this.occupantService.update(this.occupant)
                .subscribe(data => {
                    this.occupant = data;
                    this.toastr.success("Ви оновили інформацію про мешканця", "Успішно!");
                    this.onSubmit.emit(this.occupant);
                },
                err => {
                    this.toastr.error("Виникла невідома помилка на сервері", "Помилка");
                })
        }
    }

    getExemption(exemption: Exemption) {
        return exemption.name + " (" + exemption.percent + "%)";
    }

    getConsumptionNorm(consumedUtilityId: number) {
        let index = this.occupant.consumptionNorms
            .filter(c => !c.isSeasonal)
            .map(c => c.consumedUtilityId)
            .indexOf(consumedUtilityId);
        if (index == -1)
            return null;
        return this.occupant.consumptionNorms[index];
    }

    getSeasonalNorm(consumedUtilityId: number) {
        let index = this.occupant.consumptionNorms
            .filter(c => c.isSeasonal)
            .map(c => c.consumedUtilityId)
            .indexOf(consumedUtilityId);
        if (index == -1)
            return null;
        return this.occupant.consumptionNorms[index];
    }
}