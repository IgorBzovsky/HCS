import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";
import { SettingsService } from "../../services/settings.service";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    constructor(private authService: AuthService, private router: Router, private settings: SettingsService) { }
    ngOnInit() {
        if (this.authService.isInRole(this.settings.roleNames.admin)) {
            this.router.navigate(["/admin"]);
        }
        else if (this.authService.isInRole(this.settings.roleNames.provider)) {
            console.log("Provider");
            this.router.navigate(["/provider"]);
        }
        else if (this.authService.isLoggedIn()) {
            console.log("User");
            this.router.navigate(["/portal"]);
        }
        else {
            this.router.navigate(["/about"]);
        }
    }
}
