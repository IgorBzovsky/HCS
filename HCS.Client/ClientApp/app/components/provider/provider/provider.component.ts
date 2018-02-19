import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/auth.service";

@Component({
    selector: 'provider',
    templateUrl: './provider.component.html',
    styleUrls: ['../../app/app.component.css']
})

export class ProviderComponent implements OnInit {
    constructor(private authService: AuthService) { }

    ngOnInit() {

    }
}