import { Component, OnInit } from '@angular/core';
import { AuthService, getClientSettings } from '../../services/auth.service';
import { Router } from "@angular/router";
import * as Oidc from "oidc-client";

@Component({
    selector: 'auth-callback',
    template: ''
})
export class SilentCallbackComponent implements OnInit {
    constructor(private authService: AuthService) { }

    ngOnInit() {
        this.authService.silentCallback();
    }
}