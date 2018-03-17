import { Component, Input } from "@angular/core";
import { Household } from "../../../../../models/consumer";

@Component({
    selector: "occupants",
    templateUrl: "./occupants.component.html"
})
export class OccupantsComponent {
    @Input() household: Household;

    private selectTab($event: any) {
        console.log($event);
    }
}