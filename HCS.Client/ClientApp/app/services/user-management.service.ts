import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { AuthHttp } from 'angular2-jwt';

@Injectable()
export class ProviderService {
    constructor(private authHttp: AuthHttp) { }
    create(provider: any) {
        return this.authHttp.post('/api/providers', provider).map(res => res.json());
    }
}