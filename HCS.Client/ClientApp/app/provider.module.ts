import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { ProviderComponent } from './components/provider/provider/provider.component';
import { NavMenuComponent } from "./components/provider/navmenu/navmenu.component";
import { ProviderFormComponent } from "./components/provider/provider-form/provider-form.component";

import { ProviderService } from "./services/provider.service";

export const providerRoutes: Routes = [
    { path: '', redirectTo: 'profile', pathMatch: 'full' },
    { path: 'profile', component: ProviderFormComponent }
];


@NgModule({
    imports: [CommonModule, RouterModule, FormsModule],
    declarations: [
        ProviderComponent,
        NavMenuComponent,
        ProviderFormComponent
    ],
    exports: [ProviderComponent]
})
export class ProviderModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ProviderModule,
            providers: [
                ProviderService
            ]
        }
    }
}