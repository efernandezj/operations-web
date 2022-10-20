import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router'
import { IAccountAccountInfoShort } from './common/interfaces/commom.interface';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public isLogin: boolean;
  public isLoading: boolean;
  public account: IAccountAccountInfoShort;

  constructor(private auth: AuthService, private router: Router) {
    this.isLogin = this.auth.isAuthenticated();
    this.isLoading = false;
    this.account = {} as IAccountAccountInfoShort;

    // Update status
    this.auth.sessionChanged.subscribe(() => {
      this.isLogin = this.auth.isAuthenticated();
      if (!this.isLogin) {
        this.router.navigate(['/login']);
        this.account = {} as IAccountAccountInfoShort;
      }

      this.getAccount();
    });
  }


  ngOnInit() {
    this.getAccount();
  }


  public getAccount(): void {
    if (this.isLogin) {
      this.isLoading = true;
      this.auth.getAccount().subscribe({
        next: (result: any) => {
          if (result.isCorrect) {
            this.account = result.data as IAccountAccountInfoShort;
            if (this.account.role == null) this.account.role = "Role not configured";            
          } else {
            this.auth.logout();
          }
          this.isLoading = false;
        },
        error: (err: HttpErrorResponse) => {

          if (err.status !== 401) {
            this.isLoading = false;
          } else {
            throw err;
          }
        }
      });
    } else {
      this.router.navigate(['/login']);
      this.account = {} as IAccountAccountInfoShort;
    }
  }
}
