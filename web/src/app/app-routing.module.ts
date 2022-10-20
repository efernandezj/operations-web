import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/components/login.component';
import { ProfileComponent } from './pages/profile/profile/profile.component';
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent , canActivate: [AuthGuard]},
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'payroll', loadChildren: () => import('./pages/payroll/payroll.module').then(m => m.PayrollModule ), canActivate: [AuthGuard]},
  { path: 'security', loadChildren: () => import('./pages/security/security.module').then(m => m.SecurityModule ), canActivate: [AuthGuard]},
  { path: '**', pathMatch: 'full', redirectTo: 'home'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
