import { Component, OnInit } from '@angular/core';
import { ProviderService } from "../../../services/provider.service";
import { Provider } from "../../../models/provider";

@Component({
    selector: 'consumer-management',
    templateUrl: './consumer-management.component.html',
    styleUrls: ['./consumer-management.component.css']
})

export class ConsumerManagementComponent implements OnInit {

    constructor() { }

    ngOnInit() {
    }
}