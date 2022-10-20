import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DataTableDirective } from 'angular-datatables';
import { SwalClass } from 'src/app/common/classes/swal.class';
import { IBanks, IDropDownItem } from 'src/app/common/interfaces/commom.interface';
import { IEmployeeInit, IEmployeeShort, IEmployeeLong } from '../../../interfaces/payroll.interface';
import { PayrollService } from '../../../services/payroll.service';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-employee-body',
  templateUrl: './employee-body.component.html',
  styles: [
  ]
})
export class EmployeeBodyComponent implements OnInit {
  @Input() public initData!: IEmployeeInit;
  @ViewChild(DataTableDirective)
  private dtElement?: DataTableDirective;
  public dtOptions: DataTables.Settings;
  private dtTable!: DataTables.Api;
  public isSaving: boolean;
  public frmGroup: FormGroup;
  public employees: IEmployeeShort[];
  private employee?: IEmployeeLong;
  public filteredOptions?: Observable<IDropDownItem[]>;
  private isNewEntry: boolean;
  public isActive: boolean;
  public banks: IBanks[];


  constructor(private srvEmployee: PayrollService, private swal: SwalClass) {
    this.isSaving = false;
    this.isNewEntry = false;
    this.isActive = false;
    this.banks = [];
    this.frmGroup = new FormGroup({

      workdayNumber: new FormControl('', [Validators.required]),
      identificationCard: new FormControl('', [Validators.required]),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      age: new FormControl('', [Validators.min(17)]),
      jobTitle: new FormControl('', [Validators.required]),
      supervisor: new FormControl('', [Validators.required]),
      site: new FormControl('', [Validators.required]),
      birthDate: new FormControl(null, [Validators.required]),
      hireDate: new FormControl(new Date().toISOString(), [Validators.required]),
      fireDate: new FormControl(null),
      salary: new FormControl('', [Validators.required, Validators.min(17)]),
      bankName: new FormControl('', [Validators.required]),
      bacAccountNumber: new FormControl('', [Validators.required]),
      isActive: new FormControl(true)
    });
    this.frmGroup.get('isActive')?.valueChanges.subscribe((value: boolean) => {
      this.isActive = value;
    });

    this.employees = [];
    this.dtOptions = {
      pageLength: 10,
      responsive: true,
      columnDefs: [{ responsivePriority: 1, targets: 4 }] as any[],
      ajax: (dataTablesParameters: any, callback) => {
        callback({
          recordsTotal: this.employees.length,
          recordsFiltered: this.employees.length,
          data: this.employees
        });
      },
      columns: <any>[
        { id: 0, data: 'workdayNumber' },
        { id: 1, data: 'fullName' },
        { id: 2, data: 'identificationCard' },
        {
          id: 3, data: null, render: (data: any, type: any, row: IEmployeeShort) => {
            return row.isActive ? 'Enabled' : 'Disabled';
          }
        },
        {
          id: 4, data: null, sortable: false, className: 'text-center', render: (data: any, type: any, row: IEmployeeShort) => {
            return `<button type="button" data-target="${row.workdayNumber}" class="btn btn-link p-0"><i class="fas fa-pencil-alt"></i></button>`;
          }
        }
      ]
    }
  }

  ngOnInit() {
    this.frmGroup.get('bankName')?.disable();
    this.filteredOptions = this.frmGroup.get('supervisor')!.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.display;
        return name ? this._filter(name as string) : this.initData?.sups.slice();
      }),
    );
  }

  displayFn = (user: string) => {
    return user ? this.initData?.sups?.find(x => x.value.toString() == user)!.display : '';
  }

  private _filter(name: string): IDropDownItem[] {
    const filterValue = name.toLowerCase();
    return this.initData!.sups.filter(option => option.display.toLowerCase().includes(filterValue));
  }

  ngAfterViewInit(): void {
    this.dtElement?.dtInstance.then(tbl => {
      this.dtTable = tbl;
      this.dtTable.on('draw', () => this._setLinkEvents());
      this.dtTable.ajax.reload();
    });
  }

  ngOnDestroy(): void {
    this.dtTable?.destroy(true);
  }


  private _setLinkEvents() {
    const arrLinks = $('#tblResult tbody').find('button');
    this.isNewEntry = false;

    $.each(arrLinks, (key: number, btn: HTMLButtonElement) => {
      if ($(btn).attr('data-target')) {
        const key = Number($(btn).attr('data-target'));

        if (!isNaN(key)) {
          $(btn).off('click'); // Remove previews click event
          $(btn).on('click', () => {
            $(btn).prop('disabled', true).html('<i class="fas fa-sync fa-spin"></i>');

            this.closeForm().then(() => {

              this.srvEmployee.getEmployee(key).subscribe({
                next: (result: any) => {

                  if (result.isCorrect) {
                    this.employee = result.data as IEmployeeLong;

                    this.getBanks(this.employee.site.toString());

                    this.frmGroup.setValue({
                      workdayNumber: this.employee.workdayNumber,
                      identificationCard: this.employee.identificationCard,
                      firstName: this.employee.firstName,
                      lastName: this.employee.lastName,
                      age: 0,
                      jobTitle: this.employee.jobTitle,
                      supervisor: this.employee.supervisor,
                      site: this.employee.site,
                      birthDate: new Date(this.employee.birthDate),
                      hireDate: new Date(this.employee.hireDate),
                      fireDate: this.employee.fireDate == null ? '' : new Date(this.employee.fireDate),
                      salary: this.employee.salary,
                      bankName: this.employee.bankName,
                      bacAccountNumber: this.employee.bacAccountNumber,
                      isActive: this.employee.isActive
                    });

                    this.setAge(new Date(this.employee.birthDate).toString());

                    setTimeout(() => $('#divData').slideDown('slow'), 1);
                  } else {
                    this.swal.showAlert('Attention', result.Message, 'warning');
                  }
                  $(btn).prop('disabled', false).html('<i class="fas fa-pencil-alt"></i>');

                },

                error: (err: HttpErrorResponse) => {
                  $(btn).prop('disabled', false).html('<i class="fas fa-pencil-alt"></i>');
                  throw err;
                }
              });

            });
          });
        }
      }
    });

  }

  public add(): void {
    this.isNewEntry = true;
    this.closeForm().then(() => {
      $('#divData').slideDown('slow');
    });
  }

  public searchCompleted(employees: any[]): void {
    this.closeForm().then(() => {
      this.employees = employees;
      (this.employees) ? this.dtTable.ajax.reload() : this.dtTable.clear().draw();
    });
  }

  public closeForm(): Promise<void> {
    return new Promise((resolve, reject) => {
      $('#divData').slideUp('slow', () => {
        // Reset form
        this.frmGroup.reset();
        this.frmGroup.get('jobTitle')?.setValue('');
        this.frmGroup.get('bankName')?.setValue('');
        this.frmGroup.get('supervisor')?.setValue('');
        this.frmGroup.get('site')?.setValue('');
        this.frmGroup.get('isActive')?.setValue(true);
        this.frmGroup.get('hireDate')?.setValue(new Date().toISOString());
        this.frmGroup.get('bankName')?.disable();


        // Resolve promise after angular core refesh all
        setTimeout(() => resolve(), 1);
      });
    });
  }

  public getBanks(siteKey: string): void {
    this.banks = this.initData.banks.filter(b => b.siteID.toString() === siteKey)
    this.frmGroup.get('bankName')?.enable();
  }

  public formSubmit(): void {
    this.isSaving = true;
    const data = this.frmGroup.value;
    if (data.fireDate == "") data.fireDate = null;

    this.srvEmployee.postEmployee(data, this.isNewEntry).subscribe({
      next: (result: any) => {
        if (result.isCorrect) {
          this.closeForm().then(() => {
            const employee = result.data as IEmployeeShort;
            const idx = this.employees.findIndex(e => e.workdayNumber.toString() === employee.workdayNumber.toString());

            if (idx >= 0) {
              this.employees.splice(idx, 1, employee);
            } else {
              this.employees.push(employee);
            }
            this.dtTable?.ajax.reload();
            this.swal.showAlert('Attention', result.message, 'success');
          });

        } else {
          this.swal.showAlert('Attention', result.message, 'warning');
        }
      },

      error: (err: HttpErrorResponse) => {
        this.isSaving = false;
        throw err;
      }
    });

    this.isSaving = false;
  }

  public setAge(val: string): void {
    const age = new Date().getFullYear() - new Date(val).getFullYear();
    if (age >= 0 && age < 99)
      this.frmGroup.get('age')?.setValue(age);
  }
}
