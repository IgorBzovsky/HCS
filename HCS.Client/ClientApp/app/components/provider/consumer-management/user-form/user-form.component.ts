import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from "../../../../models/user";

@Component({
    selector: 'user-form',
    templateUrl: './user-form.component.html'
})
export class UserFormComponent {

    @Input() user: User;
    @Output() formSubmit = new EventEmitter();

    submit($event: User) {
        this.user = $event;
        this.formSubmit.emit(this.user);
    }
}