import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Block } from "../../../../../models/tariff";

@Component({
    selector: 'block',
    templateUrl: './block.component.html'
})
export class BlockComponent {

    @Input('group')
    public blockForm: FormGroup;
    @Input('measure-unit')
    measureUnit: string;
    @Input('block')
    public block: Block;
}