import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Household } from "../../../../models/consumer";
import { ConsumerService } from "../../../../services/consumer.service";

@Component({
    selector: 'household-form',
    templateUrl: './household-form.component.html',
    styleUrls: ['./household-form.component.css']
})

export class HouseholdFormComponent implements OnInit {
    @Input() household: Household;
    @Output() onSubmit = new EventEmitter();

    isBlocked: boolean;

    constructor(private consumerService: ConsumerService, private toastr: ToastsManager){ }

    ngOnInit() {
    }

    submit() {
        this.isBlocked = true;
        if (this.household.id == 0) {
            this.consumerService.createHousehold(this.household)
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.toastr.success('Ви створили адресу споживача', 'Успішно!');
                    this.onSubmit.emit(data);
                },
                err => {
                    this.isBlocked = false;
                    if (err['user-error']) {
                        this.toastr.error('Ви невірно вказали електронну адресу користувача.');
                    }
                    else if (err['location-error']) {
                        this.toastr.error('Ви невірно вказали домашню адресу користувача.');
                    }
                    else {
                        this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    }
                    console.log(err);
                });
        }
        else {
            /*this.providerService.update(this.provider)
                .subscribe(
                x => {
                    this.isBlocked = false;
                    this.toastr.success('Ви оновили профіль підприємства', 'Успішно!');
                    this.router.navigate(['./dashboard']);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                }
                );*/
        }
    }
}