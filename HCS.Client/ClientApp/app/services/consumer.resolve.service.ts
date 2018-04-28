import { Injectable } from '@angular/core';
import { RouterStateSnapshot, ActivatedRouteSnapshot, Router, Resolve } from '@angular/router';

import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/empty';
import { ConsumerService } from "./consumer.service";

@Injectable()
export class ConsumerResolveService implements Resolve<any> {
    constructor(private consumerService: ConsumerService, private router: Router, private toastr: ToastsManager) { }


    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.consumerService.getCurrentConsumer().catch(() => {
            this.toastr.warning("Спочатку необхідно обрати споживача", "Важлива інформація!");
            this.router.navigate(["/portal/user-consumers"]);
            return Observable.empty();
        });
    }
}