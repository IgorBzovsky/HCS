import { Component, OnInit } from "@angular/core";
import { TariffListItem, TariffDataSource } from "../../../../models/tariff";
import { TariffService } from "../../../../services/tariff.service";
import { ProviderService } from "../../../../services/provider.service";
import { Router, ActivatedRoute } from "@angular/router";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { ConsumerService } from "../../../../services/consumer.service";
import { Provider } from "../../../../models/provider";

@Component({
    selector: "tariff-list",
    templateUrl: "./tariff-list.component.html",
    styleUrls: ["./tariff-list.component.css"]
})
export class TariffListComponent implements OnInit {
    provider: Provider;
    tariffsData: TariffListItem[] = [];
    displayedColumns = ['name', 'providedUtility', 'consumerType', 'consumersQuantity', 'actions'];
    dataSource: TariffDataSource;
    isDeleteBlocked: boolean;

    constructor(private providerService: ProviderService, private tariffService: TariffService, private router: Router, private route: ActivatedRoute, private toastr: ToastsManager) { }

    ngOnInit() {
        console.log(this.route.data);
        this.route.data
            .subscribe((data: { provider: Provider }) => {
                console.log(data.provider.name);
                this.provider = data.provider;
                this.tariffService.getAllByProviderId(this.provider.id)
                    .subscribe(tariffs => {
                        this.tariffsData = tariffs;
                        this.populateTable();
                    });
            });
    }

    edit(id: number) {
        this.router.navigate(["provider/tariffs/" + id]);
    }

    delete(id: number) {
        if (confirm("Ви впевнені, що хочете видалити тариф?")) {
            this.isDeleteBlocked = true;
            this.tariffService.delete(id)
                .subscribe(data => {
                    const index = this.tariffsData.map(x => x.id).indexOf(id);
                    if (index != -1) {
                        this.tariffsData.splice(index, 1);
                    }
                    this.dataSource = new TariffDataSource(this.tariffsData);
                    this.toastr.success("Ви видалили тариф", "Успішно!");
                    this.isDeleteBlocked = false;
                },
                err => {
                    this.isDeleteBlocked = false;
                    if (err.status == 404) {
                        this.toastr.error('Такого тарифу не існує.', 'Помилка!');
                    }
                    else {
                        this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    }
                }
                );
        }
    }

    private populateTable() {
        for (let i = 0; i < this.tariffsData.length; i++) {
            this.tariffsData[i].position = i + 1;
        }
        this.dataSource = new TariffDataSource(this.tariffsData);
    }
}