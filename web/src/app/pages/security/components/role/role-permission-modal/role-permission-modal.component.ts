import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-role-permission-modal',
  templateUrl: './role-permission-modal.component.html',
  styles: [
  ]
})
export class RolePermissionModalComponent implements OnInit {
  @Input() public modalRef?: BsModalRef;
  @Output() public searchCompleted: EventEmitter<any>;
  public saving: boolean;
  
  constructor() { 
    this.searchCompleted = new EventEmitter();
    this.saving = false;
  }

  ngOnInit(): void {
  }

}
