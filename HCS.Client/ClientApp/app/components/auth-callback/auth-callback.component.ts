import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from "@angular/router";

@Component({
    selector: 'auth-callback',
    template: ''
})
export class AuthCallbackComponent implements OnInit {
    constructor(private router: Router, private authService: AuthService) { }

    ngOnInit() {
        this.authService.completeAuthentication();
        this.router.navigate([localStorage.getItem("redirectUrl")]);
    }
}