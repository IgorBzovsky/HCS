import { Component, OnInit, ViewChild } from '@angular/core';
import { ConsumerService } from "../../../services/consumer.service";
import { FormControl } from "@angular/forms";
import { Household } from "../../../models/consumer";
import { MatStepper } from "@angular/material/stepper";
import { MatHorizontalStepper } from "@angular/material/stepper";
import { SaveAddress } from "../../../models/address";

enum FormSteps { AddressForm, ConsumerForm };

@Component({
    selector: 'consumer-form',
    templateUrl: './consumer-form.component.html',
    styleUrls: ['./consumer-form.component.css']
})
export class ConsumerFormComponent implements OnInit {
    

    @ViewChild('stepper') stepper: MatStepper;
    household: Household = {
        id: 0,
        area: 0,
        hasElectricHeating: false,
        hasTowelRail: false,
        hasElectricHotplates: false,
        hasCentralGasSupply: false,
        applicationUserId: '',
        locationId: 0
    }

    addressFormControl = new FormControl();
    setAddressFormInvalid() {
        this.addressFormControl.setErrors({ 'addressFormCompleted': 'Address not completed' });
    }
    setAddressFormValid() {
        this.addressFormControl.setErrors(null);
    }
    consumerFormControl = new FormControl();
    setConsumerFormInvalid() {
        this.addressFormControl.setErrors({ 'consumerFormCompleted': 'Consumer form not completed' });
    }
    setConsumerFormValid() {
        this.consumerFormControl.setErrors(null);
    }

    constructor(private consumerService: ConsumerService) {
        this.setAddressFormInvalid();
        this.setConsumerFormInvalid();
    }

    ngOnInit() {
    }
    
    submitAddress(address: SaveAddress) {
        this.household.locationId = address.id;
        this.consumerService.getConsumerByLocationId(address.id)
            .subscribe(
            data => {
                this.household = data;
            },
            err => {
                console.log(err);
            });
        this.setAddressFormValid();
        this.stepper.next();
    }

    submitHousehold(household: Household) {
        this.setConsumerFormValid();
        this.stepper.next();
    }
}