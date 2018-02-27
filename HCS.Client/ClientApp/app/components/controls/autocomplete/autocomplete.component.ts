import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import { KeyValuePair } from "../../../models/key_value_pair";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/startWith';

@Component({
    selector: 'autocomplete',
    templateUrl: './autocomplete.component.html'
})

export class AutocompleteComponent implements OnInit {
    @Input() items: any[];
    @Input() item: any;
    @Input() placeholder: string;
    @Input() error: string;

    @Output() itemChanged = new EventEmitter();

    filteredItems: Observable<any[]>;
    control = new FormControl();
    focused = false;

    constructor() {
        this.filteredItems = this.control.valueChanges
            .startWith(null)
            .map(name => this.filterStates(name));
        
    }

    ngOnInit() {
        if (this.item) {
            this.control.setValue(this.item.name);
        }
    }

    validateChoice() {
        this.focused = false;
        if (!this.items) {
            this.control.setValue('');
            this.item = null;
            this.itemChanged.emit(this.item);
            return;
        }
        const index = this.items.map(x => x.name).indexOf(this.control.value);
        if (index === -1) {
            this.control.setValue('');
            this.item = null;
        }
        else {
            this.item = this.items[index];
        }
        this.itemChanged.emit(this.item);
    }

    filterStates(val: string) {
        if (!this.items) {
            return [];
        }
        return val ? this.items.filter(s => s.name.toLowerCase().indexOf(val.toLowerCase()) === 0)
            : this.items;
    }
}