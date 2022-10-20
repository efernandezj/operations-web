import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { SwalClass } from 'src/app/common/classes/swal.class';
import { IEmployeeInit } from '../../interfaces/payroll.interface';
import { PayrollService } from '../../services/payroll.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html'
})
export class EmployeeComponent implements OnInit {
  public loading: boolean;
  public initData: IEmployeeInit;

  constructor(private swal: SwalClass, private srvEmployee: PayrollService) {
    this.loading = true;
    this.initData = { jobs: [], sites: [], banks: [], sups: [] };
  }

  ngOnInit(): void {
    this.srvEmployee.getInitData().subscribe({
      next: (result: any) => {
        if (result.isCorrect) {
          this.initData = result.data as IEmployeeInit;
        } else {
          this.swal.showAlert('Attention!', result.message, 'warning')
        }
        this.loading = false;
      },

      error: (err: HttpErrorResponse) => {
        this.loading = false;
        throw err;
      }
    });
  }

}
