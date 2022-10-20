import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, catchError, Observable, switchMap, throwError } from 'rxjs';
import { AuthService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {
  constructor(public auth: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const accessToken = localStorage.getItem('access_token');
    const jwtHelper = new JwtHelperService();

    if ((this.auth.isEmpty(accessToken) || (accessToken && jwtHelper.isTokenExpired(accessToken))) && !req.url.includes('api/login')) {
      this.auth.logout();
    }

    return next.handle(req.clone({ setHeaders: { 'authorization': `Bearer ${accessToken}` } }));
  }


}
