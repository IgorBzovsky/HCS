import { Component, OnInit } from "@angular/core";
import { ConsumedUtility } from "../../../models/consumed-utility";
import { ConsumerInfo } from "../../../models/consumer";
import { Occupant } from "../../../models/occupant";
import 'rxjs/add/Observable/forkJoin';
import { Observable } from "rxjs/Observable";
import { ConsumerService } from "../../../services/consumer.service";
import { OccupantService } from "../../../services/occupant.service";
import { UtilityBill, SaveUtilityBill } from "../../../models/utility-bill";
import { ActivatedRoute, Router } from "@angular/router";
import { RegexService } from "../../../services/regex.service";
import { LocationService } from "../../../services/location.service";
import { Address } from "../../../models/address";
import { ToastsManager } from "ng2-toastr/ng2-toastr";

@Component({
    selector: "utility-bill-form",
    templateUrl: "./utility-bill-form.component.html"
})
export class UtilityBillFormComponent implements OnInit {

    utilityBill = new UtilityBill();
    address = new Address();
    utilityBillAddress: string;
    consumer: ConsumerInfo;

    isBlocked = false;
    
    constructor(private route: ActivatedRoute, private regexService: RegexService, private consumerService: ConsumerService, private locationService: LocationService, private toastr: ToastsManager, private router: Router) { }

    ngOnInit() {
        this.route.data
            .subscribe((data: { consumer: ConsumerInfo }) => {
                this.consumer = data.consumer;
                this.initializeUtilityBill();
                this.locationService.get(this.consumer.locationId)
                    .subscribe(address => {
                        this.address = this.locationService.mapFromLocation(address);
                        this.utilityBillAddress = this.locationService.stringifyUtilityBillAddress(this.address);
                    });
            });
    }

    initializeUtilityBill() {
        this.utilityBill.id = 0;
        this.utilityBill.month = null;
        this.utilityBill.year = null;
        this.utilityBill.isMetersReading = false;
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
        let utilityBill = this.getUtilityBill();
        this.consumerService.createUtilityBill(utilityBill)
            .subscribe(data => {
                this.isBlocked = false;
                this.toastr.success("Ви створили платіжку!", "Успішно!");
                this.router.navigate(["portal/utility-bills"]);
            },
            err => {
                this.isBlocked = false;
                this.toastr.error("Виникла помилка! Дані вказано невірно!", "Помилка!");
            })
    }

    getLinesWithMeter() {
        return this.utilityBill.utilityBillLines.filter(x => x.consumedUtility.hasMeter);
    }

    getLinesWithoutMeter() {
        return this.utilityBill.utilityBillLines.filter(x => !x.consumedUtility.hasMeter);
    }

    private getUtilityBill() {
        let utilityBill = new SaveUtilityBill();
        utilityBill.id = this.utilityBill.id;
        utilityBill.isMetersReading = false;
        utilityBill.consumerId = this.consumer.id;
        utilityBill.month = this.utilityBill.month ? this.utilityBill.month : 0;
        utilityBill.year = this.utilityBill.year ? this.utilityBill.year : 0;
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