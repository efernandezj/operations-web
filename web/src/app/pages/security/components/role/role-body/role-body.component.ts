import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { DataTableDirective } from 'angular-datatables';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IRole } from '../../../interfaces/security.interfaces';

@Component({
  selector: 'app-role-body',
  templateUrl: './role-body.component.html',
  styleUrls: ['./role-body.component.css']
})
export class RoleBodyComponent implements OnInit {
  @ViewChild(DataTableDirective)
  private dtElement?: DataTableDirective;
  private dtTable!: DataTables.Api;
  public dtOptions: DataTables.Settings;
  public modalRef?: BsModalRef;
  public frmGroup: FormGroup;
  public isActive: boolean;
  public isSaving: boolean;
  public roles: IRole[];
  // public get arrayModules(): FormArray {
  //   return (this.frmGroup.get('modules') as FormArray);
  // }


  constructor(private srvModal: BsModalService) { 
    this.roles = [];
    this.isActive = false;
    this.isSaving = false;

    this.frmGroup = new FormGroup({
      rolename: new FormControl(),
      isActive: new FormControl(true)
    });

    this.dtOptions = {
      pageLength: 10,
      responsive: true,
      columnDefs: [{ responsivePriority: 1, targets: 3 }] as any[],
      ajax: (dataTablesParameters: any, callback) => {
        callback({
          recordsTotal: (this.roles || []).length,
          recordsFiltered: (this.roles || []).length,
          data: this.roles
        });
      },
      columns: <any>[
        { id: 0, data: 'rolename' },
        { id: 1, data: 'users' },
        {
          id: 2, data: null, render: (data: any, type: any, row: any) => {
            return row.isActive ? 'Enabled' : 'Disabled';
          }
        },
        {
          id: 3, data: null, sortable: false, className: 'text-center', render: (data: any, type: any, row: any) => {
            return `<button type="button" data-target="${row.key}" class="btn btn-link p-0"><i class="fas fa-pencil-alt"></i></button>`;
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

  public addNew(): void {}

  public formSubmit(): void {}

  public closeForm(): void {}

  public searchCompleted(employees: any[]): void {
    this.modalRef?.hide();
    // this.closeForm().then(() => {
    //   this.employees = employees;
    //   if (this.employees == undefined) this.employees = [];
    //   this.dtTable.ajax.reload();
    // });
  }

  public (): void {

  }

  public showPermission(template: TemplateRef<void>): void {
    this.openModal(template);
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

            // this.closeForm().then(() => {
            //   // this.employee = this.roles.find(e => e.key === key);
            //   // this.isNew = this.roles?.username == null;

            //   // // EDIT FORM SETTING
            //   // if(!this.isNew) {
            //   //   this.frmGroup.get('username')?.enable()
            //   // }

            //   // this.frmGroup.setValue({
            //   //   workdayNumber: this.employee?.workdayNumber,
            //   //   name: this.employee?.fullName,
            //   //   email: this.employee?.email,
            //   //   username: this.employee?.username,
            //   //   isActive: this.employee?.isActive
            //   // });
            //   setTimeout(() => $('#divData').slideDown('slow'), 1);
            //   $(btn).prop('disabled', false).html('<i class="fas fa-pencil-alt"></i>');
            // });

          });
        }
      }
    });

  }
}
