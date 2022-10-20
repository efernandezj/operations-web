import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { IAccountAccountInfoShort } from '../../interfaces/commom.interface';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styles: [
  ]
})
export class NavBarComponent implements OnInit {
  @Input() public account?: IAccountAccountInfoShort;
  
  constructor(private srvLogin: AuthService) { }

  ngOnInit(): void {

  }

  toggleMenu(): void {
    $('body').toggleClass('toggle-sidebar');
  }

  logout(): void {
    this.srvLogin.logout();
  }
}
