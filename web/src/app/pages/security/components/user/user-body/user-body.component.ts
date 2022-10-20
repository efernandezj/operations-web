import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DataTableDirective } from 'angular-datatables';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SwalClass } from 'src/app/common/classes/swal.class';
import { IUser } from 'src/app/common/interfaces/commom.interface';
import { SecurityService } from '../../../services/security.service';

@Component({
  selector: 'app-user-body',
  templateUrl: './user-body.component.html'
})
export class UserBodyComponent implements OnInit {
  @ViewChild(DataTableDirective)
  private dtElement?: DataTableDirective;
  private dtTable!: DataTables.Api;
  public dtOptions: DataTables.Settings;
  public employees: IUser[];
  public employee: IUser | undefined;
  public modalRef?: BsModalRef;
  public frmGroup: FormGroup;
  public isSaving: boolean;
  public isActive: boolean;
  private isInvalidUsername: boolean;
  private isNew: boolean;

  get isUsernameInvalid() {
    return this.frmGroup.get('username')?.invalid;
  }

  constructor(private srvModal: BsModalService, private srvUser: SecurityService, private swal: SwalClass) {
    this.employees = [];
    this.isSaving = false;
    this.isActive = false;
    this.isInvalidUsername = false;
    this.isNew = false;

    this.frmGroup = new FormGroup({
      workdayNumber: new FormControl({ value: '', disabled: true }),
      name: new FormControl({ value: '', disabled: true }),
      email: new FormControl('', [Validators.required, Validators.email]),
      username: new FormControl({ value: '', disabled: true }, [Validators.required]),
      isActive: new FormControl(true)
    });

    this.frmGroup.get('isActive')?.valueChanges.subscribe((value: boolean) => { this.isActive = value });
    this.frmGroup.get('email')?.valueChanges.subscribe((value: any) => this.createUsername(value));

    this.dtOptions = {
      pageLength: 10,
      responsive: true,
      columnDefs: [{ responsivePriority: 1, targets: 4 }] as any[],
      ajax: (dataTablesParameters: any, callback) => {
        callback({
          recordsTotal: (this.employees || []).length,
          recordsFiltered: (this.employees || []).length,
          data: this.employees
        });
      },
      columns: <any>[
        { id: 0, data: 'workdayNumber' },
        { id: 1, data: 'fullName' },
        { id: 2, data: 'username' },
        { id: 3, data: 'email' },
        {
          id: 4, data: null, render: (data: any, type: any, row: IUser) => {
            return row.isActive ? 'Enabled' : 'Disabled';
          }
        },
        {
          id: 5, data: null, sortable: false, className: 'text-center', render: (data: any, type: any, row: IUser) => {
            return `<button type="button" data-target="${row.workdayNumber}" class="btn btn-link p-0"><i class="fas fa-pencil-alt"></i></button>`;
          }
        }
      ]
    }
  }

  ngOnInit(): void {
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




  public searchCompleted(employees: any[]): void {
    this.modalRef?.hide();
    this.closeForm().then(() => {
      this.employees = employees;
      if (this.employees == undefined) this.employees = [];
      this.dtTable.ajax.reload();
    });
  }

  public closeForm(): Promise<void> {
    return new Promise((resolve, reject) => {
      $('#divData').slideUp('slow', () => {
        // Reset form
        this.frmGroup.reset();
        this.frmGroup.get('username')?.disable();
        this.isInvalidUsername = false;

        // Resolve promise after angular core refesh all
        setTimeout(() => resolve(), 1);
      });
    });
  }

  public addNew(template: TemplateRef<void>): void {
    this.openModal(template);
  }

  public createUsername(value: string): void {
    let username: string = "";
    if (!this.isInvalidUsername && this.isNew){
      if (value != null) {
        username += value.split('@')[0];
      }
      this.frmGroup.get('username')?.setValue(username);
    }

  }

  public formSubmit(): void {
    this.isSaving = true;
    const data = this.frmGroup.value;
    data.workdayNumber = this.employee?.workdayNumber;
    data.username = this.frmGroup.get('username')?.value;


    this.srvUser.processUser(this.employee?.email == null, data).subscribe({
      next: (result: any) => {
        if (result.isCorrect) {
          console.log(result);
          this.swal.showAlert('Attention', result.message, 'success');
          this.closeForm().then(() => {
            const idx = this.employees.findIndex(e => e.workdayNumber === result.data.workdayNumber);
            if (idx >= 0) {
              this.employees.splice(idx, 1, result.data as IUser);
            } else {
              this.employees.push(result.data as IUser)
            }
            this.dtTable.ajax.reload();
          });
        } else {
          this.swal.showAlert('Attention', result.message, 'warning');
          if ((result.message as string).includes("username")) {
            this.frmGroup.get('username')?.enable();
            this.frmGroup.get('username')?.setErrors({ 'incorrect': true });
            this.isInvalidUsername = true;
          }
        }
      },

      error: (err: HttpErrorResponse) => {
        this.isSaving = false;
        throw err;
      }
    });
    this.isSaving = false;
  }





  private openModal(template: TemplateRef<void>): void {
    this.modalRef = this.srvModal.show(template, { backdrop: 'static' });
  }

  private _setLinkEvents() {
    const arrLinks = $('#tblResult tbody').find('button');


    $.each(arrLinks, (key: number, btn: HTMLButtonElement) => {
      if ($(btn).attr('data-target')) {
        const key = Number($(btn).attr('data-target'));

        if (!isNaN(key)) {
          $(btn).off('click'); // Remove previews click event
          $(btn).on('click', () => {
            $(btn).prop('disabled', true).html('<i class="fas fa-sync fa-spin"></i>');

            this.closeForm().then(() => {
              this.employee = this.employees.find(e => e.workdayNumber === key);
              this.isNew = this.employee?.username == null;

              // EDIT FORM SETTING
              if(!this.isNew) {
                this.frmGroup.get('username')?.enable()
              }

              this.frmGroup.setValue({
                workdayNumber: this.employee?.workdayNumber,
                name: this.employee?.fullName,
                email: this.employee?.email,
                username: this.employee?.username,
                isActive: this.employee?.isActive
              });
              setTimeout(() => $('#divData').slideDown('slow'), 1);
              $(btn).prop('disabled', false).html('<i class="fas fa-pencil-alt"></i>');
            });

          });
        }
      }
    });

  }
}
