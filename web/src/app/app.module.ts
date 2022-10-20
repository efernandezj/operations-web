import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { NgBootstrapFormValidationModule } from 'ng-bootstrap-form-validation';

import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/components/login.component';
import { HomeComponent } from './pages/home/home.component';


// Classes
import { SwalClass } from './common/classes/swal.class';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatNativeDateModule } from '@angular/material/core';
import { CommonLibModule } from './common/common-lib.module';
import { ProfileComponent } from './pages/profile/profile/profile.component';
import { TokenInterceptorService } from './services/token-interceptor.service';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SweetAlert2Module.forRoot(),
    BrowserAnimationsModule,
    CommonLibModule,
    NgBootstrapFormValidationModule.forRoot(),
    MatNativeDateModule
  ],
  providers: [SwalClass, { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
