import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { SwalClass } from 'src/app/common/classes/swal.class';
import { PayrollService } from 'src/app/pages/payroll/services/payroll.service';
import { SecurityService } from 'src/app/pages/security/services/security.service';



@Component({
  selector: 'app-search-user-modal',
  templateUrl: './search-user-modal.component.html'
})
export class UserModalComponent implements OnInit {
  @Input() public modalRef?: BsModalRef;
  @Input() public withUsernameField: boolean;
  @Output() public searchCompleted: EventEmitter<any>;
  public searching: boolean;
  public frmGroup: FormGroup;

  constructor(private srvUser: SecurityService, private swal: SwalClass) { 
    this.searchCompleted = new EventEmitter();
    this.withUsernameField = false;
    this.searching = false;
    this.frmGroup = new FormGroup({
      workday: new FormControl(''),
      username: new FormControl(''),
      name: new FormControl('')
    });
  }

  ngOnInit(): void {
  }


  formSubmit(): void {
    // Look for the employee to enable as user inside the system.
    this.searching = true;
    this.srvUser.getUsers(this.frmGroup.value).subscribe({

      next: (result: any) => {
        if (result.isCorrect) {
          this.searchCompleted.emit(result.data as any[]);
        } else {
          this.swal.showAlert('Attention', result.message, 'warning');
        }
        this.searching = false;
      },
      error: (err: HttpErrorResponse) => {
        this.searching = false;
        throw err;
      }
    });
  }

}
