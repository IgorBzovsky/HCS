import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Occupant } from "../../../../../../models/occupant";
import { Exemption } from "../../../../../../models/exemption";
import { OccupantService } from "../../../../../../services/occupant.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";

@Component({
    selector: "occupants-list",
    templateUrl: "./occupants-list.component.html"
})
export class OccupantsListComponent {
    @Input() occupants: Occupant[];
    @Output() onEdit = new EventEmitter();
    @Output() onCreate = new EventEmitter();

    private isBlocked: boolean;

    constructor(private occupantService: OccupantService, private toastr: ToastsManager) { }

    getExemption(exemption: Exemption) {
        return exemption.name + " (" + exemption.percent + "%)";
    }

    getShortName(occupant: Occupant) {
        return occupant.lastName + " " + occupant.firstName.charAt(0) + "." + occupant.middleName.charAt(0) + ".";
    }

    delete(occupant: Occupant) {
        this.isBlocked = true;
        this.occupantService.delete(occupant.id)
            .subscribe(data => {
                this.removeFromList(data)
                this.isBlocked = false;
                this.toastr.success("Ви видалили мешканця " + this.getShortName(occupant), "Успішно!");
            },
            err => {
                this.toastr.error("Виникла невідома помилка на сервері", "Помилка!");
            });
    }

    edit(id: number) {
        this.onEdit.emit(id);
    }

    create() {
        this.onCreate.emit();
    }

    private removeFromList(id: number) {
        let index = this.occupants.findIndex(o => o.id === id);
        this.occupants.splice(index, 1);
    }
}

