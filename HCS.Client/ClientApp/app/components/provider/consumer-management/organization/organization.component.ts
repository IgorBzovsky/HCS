import { Component, OnInit, ViewChild, Input } from '@angular/core';
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
import { Router, ActivatedRoute } from "@angular/router";
import { TariffService } from "../../../../services/tariff.service";
import { LocationService } from "../../../../services/location.service";
import { UserManagementService } from "../../../../services/user-management.service";

@Component({
    selector: 'organization',
    templateUrl: './organization.component.html',
    styleUrls: ['./organization.component.css']
})
export class OrganizationComponent implements OnInit {

    @ViewChild('stepper') stepper: MatStepper;

    private readonly organizationDiscriminator = "Організація";

    provider: Provider;

    user = new User();
    address = new Address();
    organization = new Organization();
    consumerCategories: KeyValuePair[];

    constructor(private consumerService: ConsumerService, private providerService: ProviderService, private toastr: ToastsManager, private router: Router, private route: ActivatedRoute, private tariffService: TariffService, private locationService: LocationService, private userManagementService: UserManagementService) {
        route.params.subscribe(p => {
            this.organization.id = p['id'];
        });
    }

    ngOnInit() {
        Observable.forkJoin([
            this.providerService.getProvider(),
            this.consumerService.getCategoriesByTypeName(this.organizationDiscriminator)])
            .subscribe(data => {
                this.provider = data[0],
                this.consumerCategories = data[1],
                this.filterTariffs()
        },
            err => {
                console.log(err);
            });
        if (this.organization.id) {
            this.consumerService.get(this.organization.id)
                .subscribe(data => {
                    this.organization = data;
                    this.setFormsValid();
                    this.locationService.get(this.organization.locationId)
                        .subscribe(data => {
                            this.address = this.locationService.mapFromLocation(data);
                        });
                    this.userManagementService.getById(this.organization.applicationUserId)
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
                    this.address.id = 0;
                    this.toastr.error("За цією адресою зареєстровано домогосподарство");
                }
                else if (data.applicationUserId !== this.user.id) {
                    this.address.id = 0;
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

    private setFormsValid() {
        this.setUserFormValid();
        this.setAddressFormValid();
        this.setOrganizationFormValid();
    }

    private filterTariffs() {
        let utilities = this.provider.providedUtilities;
        for (let i = 0; i < utilities.length; i++) {
            let tariffs = utilities[i].tariffs;
            for (let j = 0; j < tariffs.length; j++) {
                tariffs.filter(t => t.consumerType === this.organizationDiscriminator);
            }
        }
    }
}