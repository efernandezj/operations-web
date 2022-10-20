import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';



@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(public auth: AuthService) { }

  async canActivate( route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    const jwtHelper = new JwtHelperService();
    const accessToken = localStorage.getItem('access_token');


    if (this.auth.isEmpty(accessToken) || (accessToken && jwtHelper.isTokenExpired(accessToken))){
      this.auth.logout();
      return false;
    }

    return true;
  }
}

