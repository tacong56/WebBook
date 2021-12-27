import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenStorageService } from './token-storage.service';
import { StaticVaribale } from '../_helpers/static-variable';

@Injectable({
    providedIn: 'root'
})

export class AuthService  {
    constructor(private http: HttpClient, private tokenStorageService: TokenStorageService){}

    /**
     * Hàm đăng nhập
     * @param value data từ form đăng nhập
     * @returns 
     */
    login(value: any) : Observable<any> {
      const body = JSON.stringify(value);
      return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.user.login, body, StaticVaribale.httpOptions)
    }

    loginClient(value: any) : Observable<any> {
      const body = JSON.stringify(value);
      return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.user.loginClient, body, StaticVaribale.httpOptions)
    }

    logout(): void {
      this.tokenStorageService.signOut();
    }

    
    logoutClient(): void {
      this.tokenStorageService.signOutClient();
    }

    /**
     * Hàm đăng ký tài khoản
     * @param data dữ liệu từ form đăng ký
     * @returns 
     */
    register(data: any): Observable<any> {
      const body = JSON.stringify(data);
      return this.http.post(StaticVaribale.URL + StaticVaribale.PATH.user.register, body, StaticVaribale.httpOptions);
    }

    
    /**
     * Hàm thay đổi thông tin tài khoản
     * @param data dữ liệu từ form update thông tin tài khoản
     * @returns 
     */
    update(data: any): Observable<any> {
      const body = JSON.stringify(data);
      return this.http.patch(StaticVaribale.URL + StaticVaribale.PATH.user.update, body, StaticVaribale.httpOptions);
    }

    info(): Observable<any> {
      return this.http.get(StaticVaribale.URL + StaticVaribale.PATH.user.info, StaticVaribale.httpOptions);
    }

    /**
     * Hàm kiểm tra role đăng nhập với permitted Roles
     * @param allowedRoles Permitted Roles
     * @returns 
     */
    roleMatch(allowedRoles: any): boolean {
      const token = this.tokenStorageService.getToken();
      let isMatch = false;
      //Chuyển đổi token từ ASCII => base64
      let payLoad = token != null ? JSON.parse(window.atob(token.split('.')[1])) : '';
      let userRole = payLoad.user_role;

      allowedRoles.forEach((element: any, index: any) => {
        if (userRole.toLowerCase().trim() == element.toLowerCase().trim()) {
          isMatch = true;
          return false;
        }
        return true;
      });
      return isMatch;
    }
}