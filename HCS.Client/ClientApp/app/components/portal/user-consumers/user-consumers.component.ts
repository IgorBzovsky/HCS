import { Component, OnInit } from "@angular/core";
import { ConsumerService } from "../../../services/consumer.service";
import { ConsumerLocation } from "../../../models/consumer";
import { Router } from "@angular/router";
import { LocationService } from "../../../services/location.service";

@Component({
    selector: "user-consumers",
    templateUrl: "./user-consumers.component.html"
})
export class UserConsumersComponent implements OnInit {

    consumers: ConsumerLocation[] = [];
    addresses: any[] = [];

    constructor(private consumerService: ConsumerService, private router: Router, private locationService: LocationService) { }


    ngOnInit() {
        this.consumerService.getUserConsumers()
            .subscribe(consumers => {
                this.consumers = consumers;
                console.log(this.consumers);
                if (this.consumers.length === 0) {
                    this.router.navigate(["portal/consumers-error"]);
                }
                else {
                    this.convertToAddresses();
                }
            })
    }

    onConsumerChange(consumerId: number) {
        this.consumerService.currentConsumerId = consumerId;
    }

    private convertToAddresses() {
        this.consumers.forEach(consumer => {
            this.addresses.push({
                id: consumer.id,
                location: this.locationService.stringifyUtilityBillAddress(this.locationService.mapFromLocation(consumer.location))
            });
        });
    }
}