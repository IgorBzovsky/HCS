import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/auth.service";
import { trigger, animate, style, group, animateChild, query, stagger, transition } from '@angular/animations';


@Component({
    selector: 'admin',
    templateUrl: './admin.component.html',
    styleUrls: ['../../app/app.component.css']
})

export class AdminComponent implements OnInit {
    constructor(private authService: AuthService) { }

    ngOnInit() {

    }
}