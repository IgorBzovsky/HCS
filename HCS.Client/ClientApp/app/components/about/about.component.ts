import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";

@Component({
    selector: 'about',
    templateUrl: './about.component.html',
    styleUrls: ['./about.component.css']
})

export class AboutComponent implements OnInit {
    constructor(private authService: AuthService, private router: Router) { }
    ngOnInit() {
    }
    enterPortal() {
        this.router
        if (!this.authService.isLoggedIn()) {
            this.authService.startAuthentication();
            return;
        }
        if (this.authService.isInRole("admin")) {
            this.router.navigate(["/admin"]);
        }
        else if (this.authService.isInRole("user")) {
            this.router.navigate(["/portal"]);
        }
    }
}

