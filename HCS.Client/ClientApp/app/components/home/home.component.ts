import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    constructor(private authService: AuthService, private router: Router) { }
    ngOnInit() {
        console.log("Hello");
        if (this.authService.isInRole("admin")) {
            this.router.navigate(["/admin"]);
        }
        else if (this.authService.isLoggedIn()) {
            this.router.navigate(["/portal"]);
        }
        else {
            this.router.navigate(["/about"]);
        }
    }
}
