import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Household } from "../../../../../models/consumer";
import { ConsumerService } from "../../../../../services/consumer.service";

@Component({
    selector: 'household-form',
    templateUrl: './household-form.component.html'
})

export class HouseholdFormComponent implements OnInit {

    @Input() household: Household;
    @Output() formSubmit = new EventEmitter();

    isBlocked: boolean;

    constructor(private consumerService: ConsumerService, private toastr: ToastsManager) { }

    ngOnInit() {
    }

    submit() {
        this.isBlocked = true;
        if (this.household.id == 0) {
            this.consumerService.createHousehold(this.household)
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
            this.consumerService.updateHousehold(this.household)
                .subscribe(
                x => {
                    this.isBlocked = false;
                    this.toastr.success('Ви оновили інформацію про домогосподарство', 'Успішно!');
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                }
                );
        }
    }
}