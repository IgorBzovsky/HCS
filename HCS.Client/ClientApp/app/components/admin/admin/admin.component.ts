﻿import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/auth.service";

@Component({
    selector: 'admin',
    templateUrl: './admin.component.html'
})

export class AdminComponent implements OnInit {
    constructor(private authService: AuthService) { }

    ngOnInit() {

    }
}