import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Address } from "../../../models/address";
import { UtilityBill, UtilityBillLineData, UtilityBillLinesDataSource } from "../../../models/utility-bill";
import { ConsumerService } from "../../../services/consumer.service";

@Component({
    selector: "utility-bill-details",
    templateUrl: "./utility-bill-details.component.html",
    styleUrls: ["./utility-bill-details.component.css"]
})
export class UtilityBillDetailsComponent implements OnInit {
    
    utilityBill: UtilityBill = new UtilityBill();
    utilityBillLinesData: UtilityBillLineData[] = [];
    displayedColumns = ['consumedUtility', 'meterReadingStart', 'meterReadingEnd', 'consumption', 'price'];
    dataSource: UtilityBillLinesDataSource;

    constructor(private route: ActivatedRoute, private router: Router, private consumerService: ConsumerService) {
        route.params.subscribe(p => {
            this.utilityBill.id = p['id'];
        });
    }

    ngOnInit() {
        if (this.utilityBill.id) {
            this.consumerService.getUtilityBill(this.utilityBill.id)
                .subscribe(utilityBill => {
                    this.utilityBill = utilityBill;
                    this.populateTable();
                    console.log(this.utilityBillLinesData);
                },
                err => {
                    console.log(err);
                });
        }
    }

    private populateTable() {
        let lines = this.utilityBill.utilityBillLines;
        for (let i = 0; i < lines.length; i++) {
            let utilityBillLineData = new UtilityBillLineData(lines[i]);
            utilityBillLineData.position = i + 1;
            this.utilityBillLinesData.push(utilityBillLineData);
        }
        this.dataSource = new UtilityBillLinesDataSource(this.utilityBillLinesData);
    }
}