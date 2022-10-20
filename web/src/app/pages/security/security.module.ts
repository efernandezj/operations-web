import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SecurityRoutingModule } from './security-routing.module';
import { CommonLibModule } from 'src/app/common/common-lib.module';
import { DataTablesModule } from 'angular-datatables';
import { UserModalComponent } from '../../common/components/search-user-modal/search-user-modal.component';
import { NgBootstrapFormValidationModule } from 'ng-bootstrap-form-validation';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


// Ngx-Bootstrap
import { ModalModule } from 'ngx-bootstrap/modal';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

// Components
import { UserBodyComponent } from './components/user/user-body/user-body.component';
import { UserComponent } from './components/user/user.component';
import { RoleComponent } from './components/role/role.component';
import { RoleBodyComponent } from './components/role/role-body/role-body.component';
import { RolePermissionModalComponent } from './components/role/role-permission-modal/role-permission-modal.component';


@NgModule({
  declarations: [
    UserComponent,
    UserBodyComponent,
    UserModalComponent,
    RoleComponent,
    RoleBodyComponent,
    RolePermissionModalComponent
  ],
  imports: [
    CommonModule,
    CommonLibModule,
    SecurityRoutingModule,
    DataTablesModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    ButtonsModule.forRoot(),
    NgBootstrapFormValidationModule.forRoot()
  ]
})
export class SecurityModule { }
