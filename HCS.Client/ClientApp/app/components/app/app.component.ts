import { Component, ViewContainerRef, ViewEncapsulation } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";

@Component({
    selector: 'app',
    encapsulation: ViewEncapsulation.None,
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    constructor(public toastr: ToastsManager, vcr: ViewContainerRef) {
        this.toastr.setRootViewContainerRef(vcr);
    }
}
