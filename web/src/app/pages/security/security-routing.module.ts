import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/services/auth.guard';
import { RoleComponent } from './components/role/role.component';
import { UserComponent } from './components/user/user.component';

const routes: Routes = [
  { path:"user", component: UserComponent, canActivate: [AuthGuard] },
  { path:"role", component: RoleComponent, canActivate: [AuthGuard] },
  { path:"**", pathMatch:"full", redirectTo:"user" }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }
