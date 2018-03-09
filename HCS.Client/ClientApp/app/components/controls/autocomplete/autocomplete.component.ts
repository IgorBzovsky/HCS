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
    @Input() placeholder: string;
    @Input() error: string;
    @Input() items: any[] = [];
    @Output() itemChanged = new EventEmitter();

    private _item: any;

    @Input() set item(value: any) {
        this._item = value;
        if (this._item) {
            this.control.setValue(this._item.name);
        }
        else {
            this.control.setValue('');
        }
    }

    get item(): any {
        return this._item;
    }

    filteredItems: Observable<any[]>;
    control = new FormControl();

    constructor() {
        this.filteredItems = this.control.valueChanges
            .startWith(null)
            .map(name => this.filterItems(name));
    }

    ngOnInit() {
        if (this._item) {
            this.control.setValue(this.item.name);
        }
    }

    validateChoice() {
        setTimeout(() => this.validate(), 300);
    }

    private validate() {
        let name = this.control.value;
        console.log(name);
        if (!this.item && !name)
            return;
        if (this.item && this.item.name === name)
            return;

        const index = this.items.map(x => x.name).indexOf(name);
        if (index === -1) {
            this.control.setValue('');
            this.item = null;
        }
        else {
            this.item = this.items[index];
        }
        this.itemChanged.emit(this.item);
    }

    filterItems(val: string) {
        if (!this.items) {
            return [];
        }
        return val ? this.items.filter(s => s.name.toLowerCase().indexOf(val.toLowerCase()) === 0)
            : this.items;
    }
}