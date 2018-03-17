import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/auth.service";
import { JwtHelper } from "angular2-jwt/angular2-jwt";

@Component({
    selector: 'provider',
    templateUrl: './provider.component.html',
    styleUrls: ['./provider.component.css']
})

export class ProviderComponent implements OnInit {
    constructor(private authService: AuthService) { }

    ngOnInit() {

    }

    /*testToken() {
        var helper = new JwtHelper();
        console.log(helper.getTokenExpirationDate(this.authService.getAccessToken()));
        console.log(helper.isTokenExpired(this.authService.getAccessToken()));
    }*/
}