import { Component, OnInit } from '@angular/core';
import { LocationService } from "../../../services/location.service";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import { KeyValuePair } from "../../../models/key_value_pair";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/startWith';

@Component({
    selector: 'household-form',
    templateUrl: './household-form.component.html',
    styleUrls: ['./household-form.component.css']
})

export class HouseholdFormComponent implements OnInit {
    regions: KeyValuePair[] = [];
    location: KeyValuePair;
    test = {
        id: 10,
        name: 'Hello World'
    };

    filteredOptions: Observable<KeyValuePair[]>;
    myControl = new FormControl();
    focused = false;

    constructor(private locationService: LocationService) {
        this.filteredOptions = this.myControl.valueChanges
            .startWith(null)
            .map(name => this.filterStates(name));
    }

    ngOnInit() {
        this.locationService.getRegions().subscribe(regions => {
            this.regions = regions;
            console.log(this.regions);
        });
    }

    onRegionChanged(region: KeyValuePair) {
        console.log(region);
    }

    validateChoice() {
        this.focused = false;
        const index = this.regions.map(x => x.name).indexOf(this.myControl.value);
        if (index === -1) {
            this.myControl.setValue('');
        }
        console.log("Household", index);
    }

    filterStates(val: string) {
        if (!this.regions) {
            return [];
        }
        return val ? this.regions.filter(s => s.name.toLowerCase().indexOf(val.toLowerCase()) === 0)
            : this.regions;
    }
}