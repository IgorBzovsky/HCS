import { UserManager, User, UserManagerSettings } from 'oidc-client';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthService {
    private manager: UserManager = new UserManager(getClientSettings());
    private user: User;
    public redirectUrl: string;

    constructor() {
        this.manager.getUser().then(user => {
            this.user = user;
        });
    }

    isLoggedIn(): boolean {
        return this.user != null && !this.user.expired;
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
        });
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