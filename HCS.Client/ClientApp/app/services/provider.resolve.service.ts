import { Injectable } from '@angular/core';
import { RouterStateSnapshot, ActivatedRouteSnapshot, Router, Resolve } from '@angular/router';
import { ProviderService } from "./provider.service";
import { Provider } from "../models/provider";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/empty';

@Injectable()
export class ProviderResolveService implements Resolve<any> {
    constructor(private providerService: ProviderService, private router: Router, private toastr: ToastsManager) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.providerService.getProvider().catch(() => {
            this.toastr.warning("Спочатку необхідно створити постачальника", "Важлива інформація!");
            this.router.navigate(["/provider/profile"]);
            return Observable.empty();
        });
    }
}