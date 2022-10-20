import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PayrollRoutingModule } from './payroll-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgBootstrapFormValidationModule } from 'ng-bootstrap-form-validation';


import { IncidentApprovalComponent } from './components/incidentApproval/incident-approval/incident-approval.component';
import { IncidentComponent } from './components/incident/incident/incident.component';
import { EmployeeComponent } from './components/employee/employee.component';
import { CommonLibModule } from 'src/app/common/common-lib.module';
import { EmployeeBodyComponent } from './components/employee/employee-body/employee-body.component';


import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatAutocompleteModule} from '@angular/material/autocomplete';


import { ButtonsModule } from 'ngx-bootstrap/buttons';




@NgModule({
  declarations: [
    IncidentApprovalComponent,
    IncidentComponent,
    EmployeeComponent,
    EmployeeBodyComponent
  ],
  imports: [
    CommonModule,
    PayrollRoutingModule,
    CommonLibModule,
    DataTablesModule,
    FormsModule,
    ReactiveFormsModule,
    NgBootstrapFormValidationModule.forRoot(),
    ButtonsModule.forRoot(),
    MatDatepickerModule,
    MatAutocompleteModule
  ]
})
export class PayrollModule { }
