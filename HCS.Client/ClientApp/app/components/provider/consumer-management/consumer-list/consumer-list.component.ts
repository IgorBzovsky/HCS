﻿import { Component, OnInit } from '@angular/core';
import { ConsumerService } from "../../../../services/consumer.service";
import { ConsumerLocation, ConsumerDataSource, ConsumerLocationData } from "../../../../models/consumer";
import { Router, ActivatedRoute } from "@angular/router";
import { Provider } from "../../../../models/provider";
import { ProviderService } from "../../../../services/provider.service";

@Component({
    selector: 'consumer-list',
    templateUrl: './consumer-list.component.html',
    styleUrls: ['./consumer-list.component.css']
})
export class ConsumerListComponent implements OnInit {

    private readonly organizationDiscriminator = "Організація";
    private readonly householdDiscriminator = "Домогосподарство";

    consumers: ConsumerLocation[] = [];
    consumersData: ConsumerLocationData[] = [];
    displayedColumns = ['region', 'district', 'locality', 'street', 'building', 'appartment', 'actions'];
    dataSource: ConsumerDataSource;
    provider = new Provider();

    constructor(private consumerService: ConsumerService, private router: Router, private providerService: ProviderService, private route: ActivatedRoute) { }

    ngOnInit() {
        this.route.data
            .subscribe((data: { provider: Provider }) => {
                this.provider = data.provider;
                this.consumerService.getAll(this.provider.id)
                    .subscribe(
                    consumers => {
                        this.consumers = consumers;
                        this.populateTable();
                    },
                    err => {
                        console.log("Error (get consumer list)", err);
                    })
            });
    }

    edit(consumerData: ConsumerLocationData) {
        if (consumerData.consumerType === this.organizationDiscriminator) {
            this.router.navigate(["provider/consumers/organizations/" + consumerData.id]);
        }
        else if (consumerData.consumerType === this.householdDiscriminator) {
            console.log("Household");
            this.router.navigate(["provider/consumers/households/" + consumerData.id]);
        }
    }

    private populateTable() {
        for (let i = 0; i < this.consumers.length; i++) {
            let consumerData = new ConsumerLocationData(this.consumers[i]);
            consumerData.position = i + 1;
            this.consumersData.push(consumerData);
        }
        this.dataSource = new ConsumerDataSource(this.consumersData);
    }
}