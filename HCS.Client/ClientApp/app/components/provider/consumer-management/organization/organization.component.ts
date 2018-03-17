import { Component, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from "@angular/material/stepper";
import { Provider } from "../../../../models/provider";
import { User } from "../../../../models/user";
import { Address } from "../../../../models/address";
import { FormControl } from "@angular/forms";
import { ConsumerService } from "../../../../services/consumer.service";
import { ProviderService } from "../../../../services/provider.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Organization } from "../../../../models/consumer";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';
import { KeyValuePair } from "../../../../models/key_value_pair";
import { Router } from "@angular/router";

@Component({
    selector: 'organization',
    templateUrl: './organization.component.html',
    styleUrls: ['./organization.component.css']
})
export class OrganizationComponent implements OnInit {

    @ViewChild('stepper') stepper: MatStepper;

    private readonly organizationDiscriminator = "Organization";

    provider = new Provider();
    user = new User();
    address = new Address();
    organization = new Organization();
    consumerCategories: KeyValuePair[];

    constructor(private consumerService: ConsumerService, private providerService: ProviderService, private toastr: ToastsManager, private router: Router) { }

    ngOnInit() {
        Observable.forkJoin([
            this.providerService.getProvider(),
            this.consumerService.getCategoriesByTypeName(this.organizationDiscriminator)
        ]).subscribe(data => {
            this.provider = data[0],
            this.consumerCategories = data[1]
        },
            err => {
                console.log(err)
            });
        this.setFormsInvalid();
    }

    //Form submit handlers
    userFormSubmit($event: User) {
        this.user = $event;
        this.organization.applicationUserId = this.user.id;
        this.setUserFormValid();
        this.stepper.next();
    }

    addressFormSubmit($event: Address) {
        this.address = $event;
        console.log(this.address);
        this.consumerService.getConsumerByLocationId(this.address.id)
            .subscribe(
            data => {
                if (data.consumerType.name !== this.organizationDiscriminator) {
                    this.toastr.error("За цією адресою зареєстровано домогосподарство");
                }
                else if (data.applicationUserId !== this.user.id) {
                    this.toastr.error("Ця адреса закріплена за іншим споживачем");
                }
                else {
                    this.organization = data;
                    this.setAddressFormValid();
                    this.stepper.next();
                    this.setOrganizationFormValid();
                }
            },
            err => {
                if (err.status == 404) {
                    this.organization = new Organization();
                    this.organization.applicationUserId = this.user.id;
                    this.organization.locationId = this.address.id;
                    this.setAddressFormValid();
                    this.stepper.next();
                }
                else {
                    this.toastr.error("Виникла невідома помилка на сервері");
                }
            }
            );
    }

    organizationFormSubmit($event: Organization) {
        this.organization = $event;
        this.setOrganizationFormValid();
        this.router.navigate(["provider/consumers"]);
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

    //Organization form
    private organizationFormControl = new FormControl();
    private setOrganizationFormInvalid() {
        this.organizationFormControl.setErrors({ 'completed': 'Not completed' });
    }
    private setOrganizationFormValid() {
        this.organizationFormControl.setErrors(null);
    }

    private setFormsInvalid() {
        this.setUserFormInvalid();
        this.setAddressFormInvalid();
    }
}