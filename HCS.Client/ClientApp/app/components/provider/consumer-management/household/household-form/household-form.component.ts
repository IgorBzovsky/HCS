import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ConsumerService } from "../../../../../services/consumer.service";
import { KeyValuePair } from "../../../../../models/key_value_pair";
import { Household } from "../../../../../models/consumer";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { RegexService } from "../../../../../services/regex.service";

@Component({
    selector: 'household-form',
    templateUrl: './household-form.component.html'
})

export class HouseholdFormComponent implements OnInit {

    @Input() household: Household;
    @Input() consumerCategories: KeyValuePair[];
    @Input() providedUtilities: KeyValuePair[];
    @Output() formSubmit = new EventEmitter();

    isBlocked: boolean;

    constructor(private consumerService: ConsumerService, private toastr: ToastsManager, private regexService: RegexService) { }

    ngOnInit() {
    }

    onUtilityToggle(utilityId: number, name: string, $event: any) {
        if ($event.checked)
            this.household.consumedUtilities.push({ id: 0, providedUtilityId: utilityId, name: name, obligatoryPrice: null });
        else {
            var index = this.household.consumedUtilities
                .map(x => x.providedUtilityId)
                .indexOf(utilityId);
            this.household.consumedUtilities.splice(index, 1);

        }
    }

    submit() {
        this.isBlocked = true;
        if (!this.household.id) {
            this.consumerService.create(this.household)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.household = data;
                    this.toastr.success('Ви створили адресу споживача', 'Успішно!');
                    this.formSubmit.emit(data);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                });
        }
        else {
            this.consumerService.update(this.household)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.household = data;
                    this.toastr.success('Ви оновили інформацію про домогосподарство', 'Успішно!');
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

    private containsUtilities(id: number) {
        return this.household.consumedUtilities.some(c => c.providedUtilityId === id);
    }
}