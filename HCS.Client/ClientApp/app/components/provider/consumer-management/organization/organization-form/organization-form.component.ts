import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Organization } from "../../../../../models/consumer";
import { KeyValuePair } from "../../../../../models/key_value_pair";
import { ConsumerService } from "../../../../../services/consumer.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { RegexService } from "../../../../../services/regex.service";

@Component({
    selector: "organization-form",
    templateUrl: "./organization-form.component.html"
})
export class OrganizationFormComponent {

    @Input() organization: Organization;
    @Input() consumerCategories: KeyValuePair[];
    @Input() providedUtilities: KeyValuePair[];
    @Output() formSubmit = new EventEmitter();

    private isBlocked: boolean;

    constructor(private consumerService: ConsumerService, private toastr: ToastsManager, private regexService: RegexService) { }

    onUtilityToggle(utilityId: number, name: string, $event: any) {
        if ($event.checked)
            this.organization.consumedUtilities.push({ id: 0, providedUtilityId: utilityId, name: name, obligatoryPrice: null });
        else {
            var index = this.organization.consumedUtilities
                .map(x => x.providedUtilityId)
                .indexOf(utilityId);
            this.organization.consumedUtilities.splice(index, 1);

        }
    }

    submit() {
        this.isBlocked = true;
        this.organization.isDraft = true;
        if (!this.organization.id) {
            this.consumerService.create(this.organization)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.organization = data;
                    this.toastr.success('Ви зареєстрували організацію', 'Успішно!');
                    this.formSubmit.emit(data);
                },
                err => {
                    this.organization.isDraft = false;
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                });
        }
        else {
            this.consumerService.update(this.organization)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.organization = data;
                    this.toastr.success('Ви оновили інформацію про організацію', 'Успішно!');
                    this.formSubmit.emit(data);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                }
                );
        }
    }

    get isValid(): boolean {
        if (!this.organization.consumerCategory.id)
            return false;
        return true;
    }

    private containsUtilities(id: number) {
        return this.organization.consumedUtilities.some(c => c.providedUtilityId === id);
    }
}