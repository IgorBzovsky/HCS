import { Component, Input } from "@angular/core";
import { Organization } from "../../../../../models/consumer";
import { ConsumerService } from "../../../../../services/consumer.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { ProvidedUtility } from "../../../../../models/provided-utility";
import { Router } from "@angular/router";

@Component({
    selector: "organization-tariff",
    templateUrl: "./organization-tariff.component.html",
    styleUrls: ["./organization-tariff.component.css"]
})
export class OrganizationTariffComponent {

    @Input() organization: Organization;
    @Input() discriminator: string;
    @Input() providedUtilities: ProvidedUtility[];

    private isBlocked: boolean;

    constructor(private consumerService: ConsumerService, private toastr: ToastsManager, private router: Router) { }

    submit() {
        this.isBlocked = true;
        if (!this.organization.id) 
            return;
        this.organization.isDraft = true;
        this.consumerService.update(this.organization)
            .subscribe(
            data => {
                this.isBlocked = false;
                this.organization = data;
                this.toastr.success('Ви оновили інформацію про організацію', 'Успішно!');
                this.router.navigate(["provider/consumers"]);
            },
            err => {
                this.organization.isDraft = false;
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