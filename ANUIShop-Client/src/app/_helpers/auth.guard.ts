import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { TokenStorageService } from '../services/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private authService: AuthService, 
    private tokenStorageService: TokenStorageService) {  }

  canActivate(
    route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
      if (this.tokenStorageService.getToken() != null) {
        let roles = route.data['permittedRoles'] as Array<string>;
        if (roles) {
          if (this.authService.roleMatch(roles))
            return true;
          else {
            this.router.navigateByUrl('/forbidden');
            return false;
          }
        }
        else return true;
      }
      else {
        // this.router.navigate(['user/login']);
        this.router.navigateByUrl('/admin/login');
        return false;
      }
  } 
}
