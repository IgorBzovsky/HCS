import { UserManager, User, UserManagerSettings } from 'oidc-client';
import { Injectable } from '@angular/core';
import { JwtHelper } from "angular2-jwt/angular2-jwt";

@Injectable()
export class AuthService {
    private manager: UserManager = new UserManager(getClientSettings());
    private user: User;
    private roles: any;
    public redirectUrl: string;

    constructor() {
        this.manager.getUser().then(user => {
            this.user = user;
            this.updateRoles();
        });
    }

    isLoggedIn(): boolean {
        return this.user != null && !this.user.expired;
    }

    public isInRole(roleName: string) {
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
        });
    }

    private updateRoles() {
        var jwtHelper = new JwtHelper();
        var decodedToken = jwtHelper.decodeToken(this.getAccessToken());
        this.roles = decodedToken['role'];
    }
}

export function getClientSettings(): UserManagerSettings {
    return {
        authority: 'http://localhost:5002/',
        client_id: 'hcsClient',
        redirect_uri: 'http://localhost:5000/auth-callback',
        post_logout_redirect_uri: 'http://localhost:5000/',
        response_type: "id_token token",
        scope: "openid profile hcsApi",
        filterProtocolClaims: true,
        loadUserInfo: true
    };
}