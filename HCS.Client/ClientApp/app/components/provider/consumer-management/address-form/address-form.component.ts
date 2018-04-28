import { Address, LocationIncludeParent, SaveAddress } from "../../../../models/address";
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from "@angular/forms";
import { KeyValuePair } from "../../../../models/key_value_pair";
import { LocationService } from "../../../../services/location.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";

@Component({
    selector: 'address-form',
    templateUrl: './address-form.component.html',
    styleUrls: ['./address-form.component.css']
})

export class AddressFormComponent implements OnInit {
    
    regions: KeyValuePair[] = [];
    districts: KeyValuePair[] = [];
    localities: KeyValuePair[] = [];
    streets: KeyValuePair[] = [];

    isBlocked: boolean;

    @Input() address: Address;
    @Output() formSubmit = new EventEmitter();
    
    constructor(private locationService: LocationService, private toastr: ToastsManager) {
    }

    ngOnInit() {
        this.locationService.getRegions().subscribe(regions => {
            this.regions = regions;
        });
    }

    onRegionChanged(region: KeyValuePair) {
        this.address.region = region;
        this.districts = [];
        this.onDistrictChanged(null);
        if (this.address.region) {
            this.locationService.getLocationByParentId(this.address.region.id)
                .subscribe(districts => {
                    this.districts = districts;
                    console.log(this.districts);
                });
        }
    }

    onDistrictChanged(district: KeyValuePair | null) {
        this.address.district = district;
        this.localities = [];
        this.onLocalityChanged(null);
        if (this.address.district) {
            this.locationService.getLocationByParentId(this.address.district.id)
                .subscribe(localities => {
                    this.localities = localities;
                });
        }
    }

    onLocalityChanged(locality: KeyValuePair | null) {
        this.address.locality = locality;
        this.streets = [];
        this.onStreetChanged(null);
        if (this.address.locality) {
            this.locationService.getLocationByParentId(this.address.locality.id)
                .subscribe(streets => {
                    this.streets = streets;
                });
        }
    }

    onStreetChanged(street: KeyValuePair | null) {
        this.address.street = street;
    }

    submit() {
        this.isBlocked = true;
        if (!this.address.id) {
            this.locationService.create(this.mapToSaveAddress())
                .subscribe(
                data => {
                    this.isBlocked = false;
                    this.address = this.locationService.mapFromLocation(data);
                    this.toastr.success('Ви створили адресу споживача', 'Успішно!');
                    this.formSubmit.emit(this.address);
                },
                err => {
                    this.isBlocked = false;
                    if (err.status == 409) {
                        this.address = this.locationService.mapFromLocation(JSON.parse(err._body));
                        this.toastr.info("Адреса вже зареєстрована", "Інформація!");
                        this.formSubmit.emit(this.address);
                    }
                    else {
                        this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    }
                }
                );
        }
        else {
            console.log("Update");
            this.locationService.update(this.mapToSaveAddress())
                .subscribe(
                data => {
                    console.log(data);
                    this.isBlocked = false;
                    this.address = this.locationService.mapFromLocation(data);
                    this.toastr.success('Ви оновили адресу споживача', 'Успішно!');
                    this.formSubmit.emit(this.address);
                },
                err => {
                    this.isBlocked = false;
                    if (err.status == 409) {
                        this.toastr.error("Така адреса вже зареєстрована", "Помилка!");
                    }
                    else {
                        this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    }
                }
                );
        }
        
    }

    //private mapFromLocation(location: LocationIncludeParent) {
    //    this.address.id = location.id;
    //    this.address.building = location.building;
    //    this.address.appartment = location.appartment;
    //    let street = location.parent;
    //    this.address.street = new KeyValuePair(street.id, street.name);
    //    let locality = street.parent;
    //    this.address.locality = new KeyValuePair(locality.id, locality.name);
    //    let district = locality.parent;
    //    this.address.district = new KeyValuePair(district.id, district.name);
    //    let region = district.parent;
    //    this.address.region = new KeyValuePair(region.id, region.name);
    //}

    private mapToSaveAddress() {
        let saveAddress: SaveAddress = {
            id: this.address.id,
            parentId: this.address.street ? this.address.street.id : 0,
            building: this.address.building,
            appartment: this.address.appartment ? this.address.appartment : ""
        }
        return saveAddress;
    }
}