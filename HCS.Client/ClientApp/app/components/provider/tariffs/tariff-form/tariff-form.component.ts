import { Component, OnInit } from "@angular/core";
import { Validators, FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { Tariff, Block } from "../../../../models/tariff";
import { ProviderService } from "../../../../services/provider.service";
import { ToastsManager } from "ng2-toastr/ng2-toastr";
import { Router, ActivatedRoute } from "@angular/router";
import { KeyValuePair } from "../../../../models/key_value_pair";
import { RegexService } from "../../../../services/regex.service";
import { Utility } from "../../../../models/utility";
import { TariffService } from "../../../../services/tariff.service";
import { ConsumerService } from "../../../../services/consumer.service";

@Component({
    selector: "tariff-form",
    templateUrl: "./tariff-form.component.html"
})
export class TariffFormComponent {
    tariff = new Tariff();
    utilities: Utility[] = [];
    consumerTypes: KeyValuePair[];

    public tariffForm: FormGroup;

    private isBlocked: boolean;
    
    constructor(private formBuilder: FormBuilder, private tariffService: TariffService, private providerService: ProviderService, private toastr: ToastsManager, private regexService: RegexService, private route: ActivatedRoute, private router: Router, private consumerService: ConsumerService) {
        route.params.subscribe(p => {
            this.tariff.id = p['id'];
        });
    }

    ngOnInit() {
        this.isBlocked = false;
        this.providerService.getProvider()
            .subscribe(provider => {
                this.utilities = provider.providedUtilities;
                if (this.tariff.id) {
                    this.tariffService.getById(this.tariff.id)
                        .subscribe(tariff => {
                            this.tariff = tariff;
                            this.initBlockArray();
                        });
                }
        },
            err => {
                console.log(err)
            }
        );
        this.consumerService.getTypes()
            .subscribe(types => this.consumerTypes = types);
        this.tariffForm = this.formBuilder.group({
            name: [this.tariff.name, [Validators.required, Validators.pattern(this.regexService.SpacedName)]],
            subscriberFee: [this.tariff.subscriberFee, [Validators.pattern(this.regexService.Decimal)]],
            price: [this.tariff.blocks, [Validators.required, Validators.pattern(this.regexService.Decimal)]],
            providedUtilityId: [this.tariff.providedUtilityId, [Validators.required]],
            consumerTypeId: [this.tariff.consumerTypeId, [Validators.required]],
            blocks: this.formBuilder.array([])
        });
    }

    initBlock() {
        return this.formBuilder.group({
            limit: ['', [Validators.required, Validators.pattern(this.regexService.Integer)]],
            price: ['', [Validators.required, Validators.pattern(this.regexService.Decimal)]]
        });
    }

    initBlockArray() {
        const control = <FormArray>this.tariffForm.controls['blocks'];
        for (let i = 0; i < this.tariff.blocks.length; i++) {
            control.push(this.formBuilder.group({
                limit: [this.tariff.blocks[i].limit, [Validators.required, Validators.pattern(this.regexService.Integer)]],
                price: [this.tariff.blocks[i].price, [Validators.required, Validators.pattern(this.regexService.Decimal)]]
            }));
        }
    }

    addBlock() {
        this.tariff.blocks.push(new Block());
        const control = <FormArray>this.tariffForm.controls['blocks'];
        control.push(this.initBlock());
    }

    removeBlock(i: number) {
        this.tariff.blocks.splice(i, 1);
        const control = <FormArray>this.tariffForm.controls['blocks'];
        control.removeAt(i);
    }

    getMeasureUnit() {
        if (!this.tariff.providedUtilityId)
            return "";
        let index = this.utilities.map(x => x.id).indexOf(this.tariff.providedUtilityId);
        if (index == -1)
            return "";
        return this.utilities[index].measureUnit;
    }

    submit() {
        this.isBlocked = true;
        if (!this.tariff.id) {
            this.tariffService.create(this.tariff)
                .subscribe(
                x => {
                    this.isBlocked = false;
                    this.toastr.success('Ви створили тариф', 'Успішно!');
                    this.router.navigate(['provider/tariffs']);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                }
                );
        }
        else {
            this.tariffService.update(this.tariff)
                .subscribe(
                x => {
                    this.isBlocked = false;
                    this.toastr.success('Ви оновили тариф', 'Успішно!');
                    this.router.navigate(['provider/tariffs']);
                },
                err => {
                    this.isBlocked = false;
                    this.toastr.error('Виникла невідома помилка на сервері.', 'Помилка!');
                    console.log(err);
                }
                );
        }
        
    }
}