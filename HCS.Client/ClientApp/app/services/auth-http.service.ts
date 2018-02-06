import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { AppSettingsService } from './app-settings.service';
import 'rxjs/add/operator/map';

@Injectable()
export class TestService {
    constructor(private http: Http, private appSettingsService: AppSettingsService) { }

    get apiUrl(): string {
        return this.appSettingsService.appSettings.BaseUrls.Api;
    }

    get apiUrlPrefix(): string {
        return '/api'
    }

    createHeaders() {
        var headers = new Headers();
        this.createAuthorizationHeader(headers);
        return headers;
    }

    createAuthorizationHeader(headers: Headers) {
        headers.append("Authorization", "Bearer " + this.appSettingsService.appSettings.AccessToken);
        return headers;
    }

    get(url: string) {
        /*let options = new RequestOptions({ headers: this.createHeaders(), params: new URLSearchParams() });
        return this.http.get(this.apiUrl + this.apiUrlPrefix + url, {
            headers: "hjb"
        });*/
    }


}