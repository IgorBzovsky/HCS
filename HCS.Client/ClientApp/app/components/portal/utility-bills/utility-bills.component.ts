import { Component, OnInit } from "@angular/core";
import { ConsumerService } from "../../../services/consumer.service";
import { UtilityBill, SaveUtilityBill, UtilityBillListItem, UtilityBillData, UtilityBillsDataSource } from "../../../models/utility-bill";
import { ActivatedRoute, Router } from "@angular/router";
import { RegexService } from "../../../services/regex.service";
import { LocationService } from "../../../services/location.service";
import { Address } from "../../../models/address";
import { ConsumerInfo } from "../../../models/consumer";

@Component({
    selector: "utility-bills",
    templateUrl: "./utility-bills.component.html",
    styleUrls: ["./utility-bills.component.css"]
})
export class UtilityBillsComponent implements OnInit {

    address = new Address();
    consumer: ConsumerInfo;
    utilityBillAddress: string;

    utilityBills: UtilityBillListItem[] = [];
    utilityBillsData: UtilityBillData[] = [];
    displayedColumns = ['month', 'year', 'total', 'actions'];
    dataSource: UtilityBillsDataSource;

    constructor(private route: ActivatedRoute, private router: Router, private consumerService: ConsumerService, private locationService: LocationService) { }

    ngOnInit() {
        this.route.data
            .subscribe((data: { consumer: ConsumerInfo }) => {
                this.consumer = data.consumer;
                this.locationService.get(this.consumer.locationId)
                    .subscribe(address => {
                        this.address = this.locationService.mapFromLocation(address);
                        this.utilityBillAddress = this.locationService.stringifyUtilityBillAddress(this.address);
                    });
                this.consumerService.getUtilityBills(this.consumer.id)
                    .subscribe(utilityBills => {
                        this.utilityBills = utilityBills;
                        this.populateTable();
                    });
            });
    }
    
    private populateTable() {
        for (let i = 0; i < this.utilityBills.length; i++) {
            let utilityBillData = new UtilityBillData(this.utilityBills[i]);
            utilityBillData.position = i + 1;
            this.utilityBillsData.push(utilityBillData);
        }
        this.dataSource = new UtilityBillsDataSource(this.utilityBillsData);
    }

    view(id: number) {
        this.router.navigate(["portal/utility-bill/details/" + id]);
    }
}