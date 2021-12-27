import { Injectable } from '@angular/core';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'auth-user';

const TOKEN_KEY_CLIENT = 'auth-token-client';
const USER_KEY_CLIENT = 'auth-user-client';

@Injectable({
    providedIn: 'root'
})

export class TokenStorageService {
    constructor() {  }

    signOut(): void {
        this.removeToken();
        this.removeUser();
    }

    public saveToken(token: string): void {
        window.sessionStorage.removeItem(TOKEN_KEY);
        window.sessionStorage.setItem(TOKEN_KEY, token);
    }

    public getToken(): string | null {
        return window.sessionStorage.getItem(TOKEN_KEY);
    }

    public removeToken(): void {
        window.sessionStorage.removeItem(TOKEN_KEY);
    }

    public saveUser(user: any): void {
        window.sessionStorage.removeItem(USER_KEY);
        window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
    }

    public getUser(): any {
        const user = window.sessionStorage.getItem(USER_KEY);
        if(user) {
            return JSON.parse(user);
        }

        return {};
    }

    public removeUser(): void {
        window.sessionStorage.removeItem(USER_KEY);
    }

    //Client---------------------------------------------------------
    signOutClient(): void {
        this.removeTokenClient();
        this.removeUserClient();
    }

    public saveTokenClient(token: string): void {
        window.sessionStorage.removeItem(TOKEN_KEY_CLIENT);
        window.sessionStorage.setItem(TOKEN_KEY_CLIENT, token);
    }

    public getTokenClient(): string | null {
        return window.sessionStorage.getItem(TOKEN_KEY_CLIENT);
    }

    public removeTokenClient(): void {
        window.sessionStorage.removeItem(TOKEN_KEY_CLIENT);
    }

    public saveUserClient(user: any): void {
        window.sessionStorage.removeItem(USER_KEY_CLIENT);
        window.sessionStorage.setItem(USER_KEY_CLIENT, JSON.stringify(user));
    }

    public getUserClient(): any {
        const user = window.sessionStorage.getItem(USER_KEY_CLIENT);
        if(user) {
            return JSON.parse(user);
        }

        return {};
    }

    public removeUserClient(): void {
        window.sessionStorage.removeItem(USER_KEY_CLIENT);
    }
}