import { Address } from "../../../../models/address";
import { Component, OnInit, ViewChild } from '@angular/core';
import { ConsumerService } from "../../../../services/consumer.service";
import { Household } from "../../../../models/consumer";
import { MatStepper } from "@angular/material/stepper";
import { FormControl } from "@angular/forms";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { User } from "../../../../models/user";


@Component({
    selector: 'household',
    templateUrl: './household.component.html',
    styleUrls: ['./household.component.css']
})
export class HouseholdComponent implements OnInit {

    @ViewChild('stepper') stepper: MatStepper;
    householdDiscriminator = "Household";
    user: User = {
        id: '',
        email: '',
        userName: '',
        password: '',
        confirmPassword: '',
        firstName: '',
        lastName: '',
        middleName: '',
        roles: []
    };
    address: Address = {
        id: 0,
        region: null,
        district: null,
        locality: null,
        street: null,
        building: '',
        appartment: ''
    }
    household: Household = {
        id: 0,
        area: null,
        hasElectricHeating: false,
        hasTowelRail: false,
        hasElectricHotplates: false,
        hasCentralGasSupply: false,
        applicationUserId: '',
        locationId: 0
    }

    constructor(private consumerService: ConsumerService, private toastr: ToastsManager) { }
    
    ngOnInit() {
        this.setFormsInvalid();
    }

    //Form submit handlers
    userFormSubmit($event: User) {
        this.user = $event;
        this.household.applicationUserId = this.user.id;
        this.setUserFormValid();
        this.stepper.next();
    }

    addressFormSubmit($event: Address) {
        this.address = $event;
        this.household.locationId = this.address.id;
        this.consumerService.getConsumerByLocationId(this.address.id)
            .subscribe(
            data => {
                console.log(data);
                if (data.discriminator !== this.householdDiscriminator) {
                    this.toastr.error("За цією адресою зареєстрована організація");
                }
                else if (data.applicationUserId !== this.user.id) {
                    this.toastr.error("Ця адреса закріплена за іншим споживачем");
                }
                else {
                    this.household = data;
                    this.setAddressFormValid();
                    this.stepper.next();
                }
            },
            err => {
                if (err.status == 404) {
                    this.setAddressFormValid();
                    this.stepper.next();
                }
                else {
                    this.toastr.error("Виникла невідома помилка на сервері");
                }
            }
            );
    }


    //User form
    private userFormControl = new FormControl();
    private setUserFormInvalid() {
        this.userFormControl.setErrors({ 'completed': 'Not completed' });
    }
    private setUserFormValid() {
        this.userFormControl.setErrors(null);
    }

    //Address form
    private addressFormControl = new FormControl();
    private setAddressFormInvalid() {
        this.addressFormControl.setErrors({ 'completed': 'Not completed' });
    }
    private setAddressFormValid() {
        this.addressFormControl.setErrors(null);
    }

    //Household form
    private householdFormControl = new FormControl();
    private setHouseholdFormInvalid() {
        this.householdFormControl.setErrors({ 'completed': 'Not completed' });
    }
    private setHouseholdFormValid() {
        this.householdFormControl.setErrors(null);
    }

    private setFormsInvalid() {
        this.setUserFormInvalid();
        this.setAddressFormInvalid();
        this.setHouseholdFormInvalid();
    }
}