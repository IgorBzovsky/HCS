import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MdMenuModule, MdButtonModule, MdIconModule, MdCardModule, MdSidenavModule, MdToolbarModule, MdListModule } from '@angular/material';

import { MainComponent } from "./components/portal/main/main.component";
import { MoreInfoComponent } from "./components/portal/more-info/more-info.component";
import { PortalComponent } from './components/portal/portal/portal.component';

export const portalRoutes: Routes = [
    { path: '', redirectTo: 'main', pathMatch: 'full' },
    { path: 'main', component: MainComponent },
    { path: 'more-info', component: MoreInfoComponent }
];

@NgModule({
    imports: [
        RouterModule,
        MdMenuModule,
        MdButtonModule,
        MdIconModule,
        MdCardModule,
        MdSidenavModule,
        MdToolbarModule,
        MdListModule
    ],
    declarations: [
        PortalComponent,
        MainComponent,
        MoreInfoComponent
    ],
    exports: [PortalComponent]
})
export class PortalModule {
}