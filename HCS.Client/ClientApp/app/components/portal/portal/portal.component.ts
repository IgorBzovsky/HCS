import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/auth.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ConsumerLocation } from "../../../models/consumer";
import { ConsumerService } from "../../../services/consumer.service";

@Component({
    selector: 'portal',
    templateUrl: './portal.component.html',
    styleUrls: ['./portal.component.css']
})

export class PortalComponent implements OnInit {

    consumers: ConsumerLocation[] = [];

    constructor(private authService: AuthService, private consumerService: ConsumerService, private route: ActivatedRoute, private router: Router) { }

    ngOnInit() {
        this.route.data
            .subscribe((data: { consumers: ConsumerLocation[] }) => {
                this.consumers = data.consumers;
                if (this.consumers.length === 0) {
                    this.router.navigate(["/portal/consumers-error"]);
                }
                else if (this.consumers.length === 1) {
                    this.consumerService.currentConsumerId = this.consumers[0].id;
                }
            },
            err => {
                console.log(err)
            });
    }
}
