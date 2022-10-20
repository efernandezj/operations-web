import { EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BaseService } from '../common/services/base.service';
import { JwtHelperService } from "@auth0/angular-jwt";
import { AuthenticatedResponse } from '../common/interfaces/auth.interface';

const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  
  public sessionChanged: EventEmitter<void> = new EventEmitter();


  public getAccount(): Observable<Object> {
    return this.httpGet("login");
  }

  public postLogin(data: any): Observable<Object> {
    return this.httpPost("login", data);
  }

  public setSession(authResponese: AuthenticatedResponse): void {
    localStorage.setItem('access_token', authResponese.accessToken);
    localStorage.setItem('refresh_token', authResponese.refreshToken);
    // Change session state
    this.sessionChanged.emit();
  }

  public getToken(): any {
    return localStorage.getItem('access_token');
  }
  
  public isAuthenticated(): boolean {
    const access_token = this.getToken();
    return (access_token !== null && access_token !== undefined && access_token !== '' && !helper.isTokenExpired(access_token));
  }

  public logout(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');

    // Change session state
    this.sessionChanged.emit();
  }

  public refreshToken(data: any): Observable<Object> {
    return this.httpPost('token/refresh', data);
  }

  public isEmpty(val: any) {
    return (val === undefined || val == null || val.length <= 0) ? true : false;
  }
}