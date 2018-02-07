import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminComponent } from './components/admin/admin/admin.component';
import { MainComponent } from "./components/admin/main/main.component";
import { MoreInfoComponent } from "./components/admin/more-info/more-info.component";

export const adminRoutes: Routes = [
    { path: '', redirectTo: 'main', pathMatch: 'full' },
    { path: 'main', component: MainComponent },
    { path: 'more-info', component: MoreInfoComponent }
];


@NgModule({
    imports: [RouterModule],
    declarations: [
        AdminComponent,
        MainComponent,
        MoreInfoComponent
    ],
    exports: [AdminComponent]
})
export class AdminModule {
}