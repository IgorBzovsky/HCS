import { Component, OnInit } from '@angular/core';
import { ConsumerService } from "../../../../services/consumer.service";
import { ConsumerLocation, ConsumerDataSource, ConsumerLocationData } from "../../../../models/consumer";

@Component({
    selector: 'consumer-list',
    templateUrl: './consumer-list.component.html',
    styleUrls: ['./consumer-list.component.css']
})
export class ConsumerListComponent implements OnInit {

    consumers: ConsumerLocation[];
    consumersData: ConsumerLocationData[] = [];
    displayedColumns = ['region', 'district', 'locality', 'street', 'building', 'appartment', 'actions'];
    dataSource: ConsumerDataSource;

    constructor(private consumerService: ConsumerService) { }
    consumer: ConsumerLocation[];
    ngOnInit() {
        this.consumerService.getAll()
            .subscribe(
            data => {
                this.consumers = data;
                this.populateTable();
                this.dataSource = new ConsumerDataSource(this.consumersData);
            },
            err => {
                console.log("Error (get consumer list)", err);
            })
        this.consumerService.getAll().subscribe(consumers => {
            this.consumers = consumers;
        });
    }

    private populateTable() {
        for (let i = 0; i < this.consumers.length; i++) {
            let consumerData = new ConsumerLocationData(this.consumers[i]);
            consumerData.position = i + 1;
            this.consumersData.push(consumerData);
        }
    }
}