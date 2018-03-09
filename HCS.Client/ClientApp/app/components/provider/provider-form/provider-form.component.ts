﻿import * as _ from 'underscore';
import { Component, OnInit } from '@angular/core';
import { LocationService } from "../../../services/location.service";
import { Observable } from "rxjs/Observable";
import { ProviderService } from "../../../services/provider.service";
import { Router } from "@angular/router";
import { SaveProvider, Provider } from "../../../models/provider";
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import 'rxjs/add/Observable/forkJoin';



@Component({
    selector: 'provider-form',
    templateUrl: './provider-form.component.html',
    styleUrls: ['./provider-form.component.css']
})
export class ProviderFormComponent implements OnInit {
    isBlocked: boolean;
    regions: any[];
    utilities: any[];
    provider: SaveProvider = {
        id: 0,
        name: "",
        locationId: 0,
        providedUtilities: []
    };

    constructor(private providerService: ProviderService, private locationService: LocationService, private router: Router, private toastr: ToastsManager) { }

    ngOnInit() {
        Observable.forkJoin([
            this.locationService.getRegions(),
            this.providerService.getUtilities(),
            this.providerService.getProvider()
        ]).subscribe(data => {
                this.regions = data[0],
                this.utilities = data[1],
                this.setProvider(data[2])
            },
            err => {
                console.log(err)
            }
        );
    }

    onUtilityToggle(utilityId: number, $event: any) {
        if ($event.checked)
            this.provider.providedUtilities.push(utilityId);
        else {
            var index = this.provider.providedUtilities.indexOf(utilityId);
            this.provider.providedUtilities.splice(index, 1);
            
        }
    }

    submit() {
        this.isBlocked = true;
        if (this.provider.id == 0) {
            this.providerService.create(this.provider)
                .subscribe(
                x => {
                    this.isBlocked = false;
                    this.toastr.success('Ви створили профіль підприємства', 'Успішно!');
                    this.router.navigate(['./dashboard']);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                }
            );
        }
        else {
            this.providerService.update(this.provider)
                .subscribe(
                x => {
                    this.isBlocked = false;
                    this.toastr.success('Ви оновили профіль підприємства', 'Успішно!');
                    this.router.navigate(['./dashboard']);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                }
            );
        }
        
    }

    private setProvider(provider: Provider) {
        if (!provider) {
            return;
        } 
        this.provider.id = provider.id;
        this.provider.name = provider.name;
        this.provider.locationId = provider.location.id;
        this.provider.providedUtilities = _.pluck(provider.providedUtilities, 'id');
    }
}