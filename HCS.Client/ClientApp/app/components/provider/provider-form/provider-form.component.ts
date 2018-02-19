import { Component, OnInit } from '@angular/core';
import { ProviderService } from "../../../services/provider.service";

@Component({
    selector: 'provider-form',
    templateUrl: './provider-form.component.html'
})
export class ProviderFormComponent implements OnInit {
    regions: any[];
    utilities: any[];
    provider: any = {
        providedUtilities: []
    };

    constructor(private providerService: ProviderService) { }

    ngOnInit() {
        this.providerService.getRegions().subscribe(regions => this.regions = regions);
        this.providerService.getUtilities().subscribe(utilities => this.utilities = utilities);
    }

    onUtilityToggle(utilityId: number, $event: any) {
        if ($event.target.checked)
            this.provider.providedUtilities.push(utilityId);
        else {
            var index = this.provider.providedUtilities.indexOf(utilityId);
            this.provider.providedUtilities.splice(index, 1);
        }
    }

    submit() {
        this.providerService.create(this.provider)
            .subscribe(x => console.log(x));
    }
}