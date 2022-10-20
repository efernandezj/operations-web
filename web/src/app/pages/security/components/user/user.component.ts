import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html'
})
export class UserComponent implements OnInit {
  public loading: boolean;
  constructor() {
    this.loading = false;
   }

  ngOnInit(): void {
  }

}
