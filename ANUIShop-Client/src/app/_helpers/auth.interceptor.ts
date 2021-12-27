import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { TokenStorageService } from '../services/token-storage.service';
import { Router } from '@angular/router';

const TOKEN_HEADER_KEY  = 'Authorization';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private token: TokenStorageService, private tokenStorageService: TokenStorageService, 
    private router: Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authRequest = request;
    const token = this.token.getToken();
    const tokenClient = this.token.getTokenClient();
    const href = window.location.href.includes('/admin');
    if(href) {
      if(token != null) {
        authRequest = request.clone({ headers: request.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + token) });
  
        return next.handle(authRequest).pipe(
          tap(
            succ => {
  
            },
            err => {
              if (err.status == 401) {
                this.tokenStorageService.signOut();
                this.router.navigateByUrl('admin/login');
              }
              else if (err.status == 403)
                this.router.navigateByUrl('/forbidden');
            }
          )
        )
      }
      else {
        return next.handle(authRequest);
      }
    }
    else {
      if(tokenClient != null) {
        authRequest = request.clone({ headers: request.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + tokenClient) });
        return next.handle(authRequest)
      }
      else {
        return next.handle(authRequest);
      }
    }
  }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
]
