import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/services/auth.guard';
import { EmployeeComponent } from './components/employee/employee.component';
import { IncidentComponent } from './components/incident/incident/incident.component';
import { IncidentApprovalComponent } from './components/incidentApproval/incident-approval/incident-approval.component';

const routes: Routes = [
  { path: 'employee', component: EmployeeComponent, canActivate: [AuthGuard] },
  { path: 'incident', component: IncidentComponent, canActivate: [AuthGuard] },
  { path: 'incidentApproval', component: IncidentApprovalComponent, canActivate: [AuthGuard] },
  { path: '**', pathMatch: 'full', redirectTo: 'employee' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PayrollRoutingModule { }