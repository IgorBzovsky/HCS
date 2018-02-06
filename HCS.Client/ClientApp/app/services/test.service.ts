import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { AuthHttp } from 'angular2-jwt';
import 'rxjs/add/operator/map';

@Injectable()
export class TestService {
    constructor(private http: Http, private authHttp: AuthHttp) { }
    getTests() {
        return this.authHttp.get('http://localhost:5001/identity').map(res => res.json());
    }
}