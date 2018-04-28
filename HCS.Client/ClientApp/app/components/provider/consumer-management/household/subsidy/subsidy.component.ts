import { Component, Input, Output, EventEmitter } from "@angular/core";
import { ConsumerService } from "../../../../../services/consumer.service";
import { KeyValuePair } from "../../../../../models/key_value_pair";
import { Household } from "../../../../../models/consumer";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { RegexService } from "../../../../../services/regex.service";
import { ConsumedUtility } from "../../../../../models/consumed-utility";
import { ProvidedUtility } from "../../../../../models/provided-utility";


@Component({
    selector: "subsidy",
    templateUrl: "./subsidy.component.html"
})
export class SubsidyComponent {
    @Input() household: Household;
    @Input() providedUtilities: ProvidedUtility[];
    @Output() formSubmit = new EventEmitter();
    isBlocked: boolean;

    constructor(private consumerService: ConsumerService, private toastr: ToastsManager, private regexService: RegexService) { }

    submit() {
        this.isBlocked = true;
        this.consumerService.update(this.household)
            .subscribe(
            data => {
                this.isBlocked = false;
                this.household = data;
                console.log(data);
                this.toastr.success('Ви оновили інформацію про субсидії', 'Успішно!');
                this.formSubmit.emit(data);
            },
            err => {
                this.isBlocked = false;
                this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                console.log(err);
            }
            );
    }

    getConsumedUtilities() {
        let utilities: ConsumedUtility[] = [];
        this.household.consumedUtilities.forEach(u => {
            if (this.providedUtilities.some(p => p.id === u.providedUtilityId))
                utilities.push(u);
        });
        return utilities;
    }

    getConsumedUtilityById(id: number) {
        return this.household.consumedUtilities.find(c => c.id === id);
    }
}

