import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit {
  public loading: boolean;

  constructor() {
    this.loading = false;
   }

  ngOnInit(): void {
  }

}
