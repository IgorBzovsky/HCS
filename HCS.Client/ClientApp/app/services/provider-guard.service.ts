import { Injectable } from '@angular/core';
import { CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { SettingsService } from "./settings.service";

@Injectable()
export class ProviderGuardService implements CanActivate {
    constructor(private authService: AuthService, private router: Router, private settings: SettingsService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (!this.authService.isLoggedIn()) {
            this.authService.startAuthentication();
            return false;
        }
        if (this.authService.isInRole(this.settings.roleNames.provider)) {
            return true;
        }
        return false;
    }
}