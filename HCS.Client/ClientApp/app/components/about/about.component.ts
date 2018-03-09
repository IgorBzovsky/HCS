import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";
import { SettingsService } from "../../services/settings.service";

@Component({
    selector: 'about',
    templateUrl: './about.component.html',
    styleUrls: ['./about.component.css']
})

export class AboutComponent implements OnInit {
    constructor(private authService: AuthService, private router: Router, private settings: SettingsService) { }
    ngOnInit() {
    }
    enterPortal() {
        if (!this.authService.isLoggedIn()) {
            this.authService.startAuthentication();
            return;
        }
        if (this.authService.isInRole(this.settings.RoleNames.admin)) {
            this.router.navigate(["/admin"]);
        }
        else if (this.authService.isInRole(this.settings.RoleNames.provider)) {
            this.router.navigate(["/provider"]);
        }
        else if (this.authService.isInRole(this.settings.RoleNames.consumer)) {
            this.router.navigate(["/portal"]);
        }
    }
}

