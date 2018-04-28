import * as _ from 'underscore';
import { Component, OnInit } from "@angular/core";
import { ConsumerInfo } from "../../../models/consumer";
import { ActivatedRoute, Router } from "@angular/router";
import { RegexService } from "../../../services/regex.service";
import { SaveUtilityBill, UtilityBill } from "../../../models/utility-bill";
import { ConsumerService } from "../../../services/consumer.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";

@Component({
    selector: "meter-readings",
    templateUrl: "./meter-readings.component.html"
})
export class MeterReadingsComponent implements OnInit {

    utilityBill = new UtilityBill();
    consumer: ConsumerInfo;

    isBlocked = false;

    constructor(private route: ActivatedRoute, private regexService: RegexService, private consumerService: ConsumerService, private toastr: ToastsManager, private router: Router) { }

    ngOnInit() {
        this.route.data
            .subscribe((data: { consumer: ConsumerInfo }) => {
                this.consumer = data.consumer;
                this.initializeUtilityBill();
                this.consumerService.getMetersReading(this.consumer.id)
                    .subscribe(data => {
                        this.mapToUtilityBill(data);
                        console.log("data: ", this.utilityBill);
                    }); 
            });
    }

    initializeUtilityBill() {
        this.utilityBill.id = 0;
        this.utilityBill.month = 0;
        this.utilityBill.year = 0;
        this.utilityBill.isMetersReading = true;
        this.utilityBill.consumerId = this.consumer.id;
        for (let i = 0; i < this.consumer.consumedUtilities.length; i++) {
            let utility = this.consumer.consumedUtilities[i];
            this.utilityBill.utilityBillLines.push({
                id: 0,
                consumedUtility: utility,
                meterReadingEnd: 0,
                meterReadingStart: 0,
                price: 0
            });
        }
    }

    submit() {
        this.isBlocked = true;
        let utilityBill = this.setUtilityBill();
        if (!this.utilityBill.id) {
            this.consumerService.createMetersReading(utilityBill)
                .subscribe(data => {
                    this.isBlocked = false;
                    this.toastr.success("Ви зареєстрували показники лічильників", "Успішно!");
                    this.router.navigate(["portal/dashboard"]);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error("Виникла невідома помилка на сервері", "Помилка!");
                })
        }
        else {
            this.consumerService.updateMetersReading(utilityBill)
                .subscribe(data => {
                    this.isBlocked = false;
                    this.toastr.success("Ви оновили показники лічильників", "Успішно!");
                    this.router.navigate(["portal/dashboard"]);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error("Виникла невідома помилка на сервері", "Помилка!");
                })
        }
    }

    private mapToUtilityBill(utilityBill: UtilityBill) {
        this.utilityBill.id = utilityBill.id;
        this.utilityBill.isMetersReading = true;
        this.utilityBill.consumerId = utilityBill.consumerId;
        utilityBill.utilityBillLines.forEach(u => {
            let line = this.utilityBill.utilityBillLines.find(x => x.consumedUtility.id === u.consumedUtility.id);
            if (line) {
                line.id = u.id;
                line.meterReadingEnd = u.meterReadingEnd;
            }
        });
    }

    private setUtilityBill() {
        let utilityBill = new SaveUtilityBill();
        utilityBill.id = this.utilityBill.id;
        utilityBill.isMetersReading = true;
        utilityBill.consumerId = this.consumer.id;
        this.utilityBill.utilityBillLines.forEach(u => {
            utilityBill.utilityBillLines.push({
                id: u.id,
                meterReadingEnd: u.meterReadingEnd,
                consumedUtilityId: u.consumedUtility.id
            })
        });
        return utilityBill;
    }
}