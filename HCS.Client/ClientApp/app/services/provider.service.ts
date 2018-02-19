import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class ProviderService {
    constructor(private http: Http) { }
    getUtilities() {
        return this.http.get('/api/utilities').map(res => res.json());
    }
    getRegions() {
        return this.http.get('/api/regions').map(res => res.json());
    }
    create(provider: any) {
        return this.http.post('/providers', provider).map(res => res.json());
    }
}