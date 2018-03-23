import { Component, Input } from "@angular/core";
import { Household } from "../../../../../models/consumer";
import { ConsumerService } from "../../../../../services/consumer.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { ProvidedUtility } from "../../../../../models/provided-utility";
import { Router } from "@angular/router";

@Component({
    selector: "household-tariff",
    templateUrl: "./household-tariff.component.html",
    styleUrls: ["./household-tariff.component.css"]
})
export class HouseholdTariffComponent {

    @Input() household: Household;
    @Input() providedUtilities: ProvidedUtility[];
    @Input() discriminator: string;

    private isBlocked: boolean;

    constructor(private consumerService: ConsumerService, private toastr: ToastsManager, private router: Router) { }

    submit() {
        this.isBlocked = true;
        if (!this.household.id)
            return;
        this.household.isDraft = true;
        this.consumerService.update(this.household)
            .subscribe(
            data => {
                this.isBlocked = false;
                this.household = data;
                this.toastr.success('Ви оновили інформацію про домогосподарство', 'Успішно!');
                this.router.navigate(["provider/consumers"]);
            },
            err => {
                this.household.isDraft = false;
                this.isBlocked = false;
                this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                console.log(err);
            }
            );
    }

    getTariffs(utilityId: number) {
        let index = this.providedUtilities.map(u => u.id).indexOf(utilityId);
        if (index === -1)
            return [];
        return this.providedUtilities[index].tariffs.filter(x => x.consumerType === this.discriminator);
    }
}