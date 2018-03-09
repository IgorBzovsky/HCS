import { UserManager, User, UserManagerSettings } from 'oidc-client';
import { Injectable } from '@angular/core';
import { JwtHelper } from "angular2-jwt/angular2-jwt";
import { SettingsService } from "./settings.service";

@Injectable()
export class AuthService {
    private manager: UserManager;
    private user: User;
    private roles: any;
    private profile = {
        email: ""
    }
    public redirectUrl: string;

    constructor() {
        this.manager = new UserManager(getClientSettings());
        this.manager.getUser().then(user => {
            this.user = user;
            if (this.isLoggedIn()) {
                this.updateRoles();
                this.updateProfile();
            }
        });
        this.manager.events.addAccessTokenExpiring(() => {
            this.manager.signinSilent().then(user => {
                this.user = user;
                console.log(this.user);
            });
            console.log("Token expiring");
        });
    }

    isLoggedIn(): boolean {
        return this.user != null && !this.user.expired;
    }

    public isInRole(roleName: string) {
        if (!this.roles) {
            return false;
        }
        return this.roles.indexOf(roleName) > -1;
    }

    getClaims(): any {
        return this.user.profile;
    }

    getAuthorizationHeaderValue(): string {
        return `${this.user.token_type} ${this.user.access_token}`;
    }

    getAccessToken(): string {
        return `${this.user.access_token}`;
    }

    startAuthentication(): Promise<void> {
        return this.manager.signinRedirect();
    }

    completeAuthentication(): Promise<void> {
        return this.manager.signinRedirectCallback().then(user => {
            this.user = user;
            this.updateRoles();
            this.updateProfile();
        });
    }

    silentCallback(): Promise<void> {
        return this.manager.signinSilentCallback().then(user => {
            console.log("Silent refresh!");
        });
    }

    startSignout() {
        this.manager.getUser().then(user => {
            return this.manager.signoutRedirect({ id_token_hint: user.id_token }).then(resp => {
                console.log('signed out', resp);
                setTimeout(5000, () => {
                    console.log('testing to see if fired...');
                });
            }).catch(function (err) {
                console.log(err);
            });
        });
    };



    completeSignout() {
        this.manager.signoutRedirectCallback().then(function (response) {
            console.log('signed out', response);
        }).catch(function (err) {
            console.log(err);
        });
    };

    getProfile() {
        return this.profile;
    }

    private updateRoles() {
        var jwtHelper = new JwtHelper();
        var decodedToken = jwtHelper.decodeToken(this.getAccessToken());
        this.roles = decodedToken['role'];
    }

    private updateProfile() {
        var jwtHelper = new JwtHelper();
        var decodedToken = jwtHelper.decodeToken(this.getAccessToken());
        this.profile.email = decodedToken['email'];
    }
}

export function getClientSettings(): UserManagerSettings {
    var settings = new SettingsService();
    return {
        authority: 'http://localhost:5002/',
        client_id: 'hcsClient',
        redirect_uri: 'http://localhost:5000/auth-callback',
        post_logout_redirect_uri: 'http://localhost:5000/',
        response_type: "id_token token",
        scope: "openid profile hcsApi",
        filterProtocolClaims: true,
        loadUserInfo: true,
        //automaticSilentRenew: true,
        silent_redirect_uri: 'http://localhost:5000/silent-callback'
    };
}