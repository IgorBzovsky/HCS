import { Address } from "../../../../models/address";
import { Component, OnInit, ViewChild } from '@angular/core';
import { ConsumerService } from "../../../../services/consumer.service";
import { MatStepper } from "@angular/material/stepper";
import { FormControl } from "@angular/forms";
import { Provider } from "../../../../models/provider";
import { ProviderService } from "../../../../services/provider.service";
import { Household } from "../../../../models/consumer";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { User } from "../../../../models/user";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';
import { KeyValuePair } from "../../../../models/key_value_pair";
import { Occupant } from "../../../../models/occupant";
import { OccupantService } from "../../../../services/occupant.service";
import { Exemption } from "../../../../models/exemption";
import { TariffService } from "../../../../services/tariff.service";
import { UserManagementService } from "../../../../services/user-management.service";
import { Router, ActivatedRoute } from "@angular/router";
import { LocationService } from "../../../../services/location.service";


@Component({
    selector: 'household',
    templateUrl: './household.component.html',
    styleUrls: ['./household.component.css']
})
export class HouseholdComponent implements OnInit {

    @ViewChild('stepper') stepper: MatStepper;

    householdDiscriminator = "Домогосподарство";

    provider = new Provider();
    user = new User();
    address = new Address();
    household = new Household();
    exemptions: Exemption[] = [];
    consumerCategories: KeyValuePair[];

    constructor(private consumerService: ConsumerService, private providerService: ProviderService, private toastr: ToastsManager, private router: Router, private route: ActivatedRoute, private occupantService: OccupantService, private tariffService: TariffService, private userManagementService: UserManagementService, private locationService: LocationService) {
        route.params.subscribe(p => {
            this.household.id = p['id'];
        });
    }
    
    ngOnInit() {
        Observable.forkJoin([
            this.providerService.getProvider(),
            this.consumerService.getExemptions(),
            this.consumerService.getCategoriesByTypeName(this.householdDiscriminator)
        ]).subscribe(data => {
            this.provider = data[0],
            this.exemptions = data[1],
            this.consumerCategories = data[2]
        },
            err => {
                console.log(err)
            });
        if (this.household.id) {
            this.consumerService.get(this.household.id)
                .subscribe(data => {
                    console.log(data);
                    this.household = data;
                    this.setFormsValid();
                    this.locationService.get(this.household.locationId)
                        .subscribe(data => {
                            this.address = this.locationService.mapFromLocation(data);
                        });
                    this.userManagementService.getById(this.household.applicationUserId)
                        .subscribe(data => {
                            this.user = data;
                        });
                });
        }
        else {
            this.setFormsInvalid();
        }
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
        this.consumerService.getConsumerByLocationId(this.address.id)
            .subscribe(
            data => {
                if (data.consumerType.name !== this.householdDiscriminator) {
                    this.toastr.error("За цією адресою зареєстрована організація");
                }
                else if (data.applicationUserId !== this.user.id) {
                    this.toastr.error("Ця адреса закріплена за іншим споживачем");
                }
                else {
                    this.household = data;
                    this.setAddressFormValid();
                    this.stepper.next();
                    this.setHouseholdFormValid();
                }
            },
            err => {
                if (err.status == 404) {
                    this.household = new Household();
                    this.household.locationId = this.address.id;
                    this.household.applicationUserId = this.user.id;
                    this.setAddressFormValid();
                    this.stepper.next();
                }
                else {
                    this.toastr.error("Виникла невідома помилка на сервері");
                }
            }
            );
    }

    householdFormSubmit($event: Household) {
        this.household = $event;
        this.setHouseholdFormValid();
        this.stepper.next();
    }

    subsidyFormSubmit($event: Household) {
        this.household = $event;
        this.setSubsidyFormValid();
        this.stepper.next();
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

    //Subsidy form
    private subsidyFormControl = new FormControl();
    private setSubsidyFormInvalid() {
        this.subsidyFormControl.setErrors({ 'completed': 'Not completed' });
    }
    private setSubsidyFormValid() {
        this.subsidyFormControl.setErrors(null);
    }

    //Occupants form
    private occupantsFormControl = new FormControl();
    private setOccupantsFormInvalid() {
        this.occupantsFormControl.setErrors({ 'completed': 'Not completed' });
    }
    private setOccupantsFormValid() {
        this.occupantsFormControl.setErrors(null);
    }

    private setFormsValid() {
        this.setUserFormValid();
        this.setAddressFormValid();
        this.setHouseholdFormValid();
    }

    private setFormsInvalid() {
        this.setUserFormInvalid();
        this.setAddressFormInvalid();
        this.setHouseholdFormInvalid();
    }
}