import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule} from '@angular/forms'

// Angular material
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';


import { LoadingComponent } from './components/loading/loading.component';
import { CollapseableDirective } from './directives/collapseable.directive';
import { SidemenuComponent } from './components/sidemenu/sidemenu.component';
import { NavBarComponent } from './components/navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { SearchComponent } from './components/search-profile/search.component';






@NgModule({
  declarations: [
    LoadingComponent,
    SearchComponent,
    SidemenuComponent,
    NavBarComponent,
    CollapseableDirective
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    RouterModule
  ],
  exports: [
    LoadingComponent,
    SearchComponent,
    SidemenuComponent,
    NavBarComponent,
    CollapseableDirective
  ]
})
export class CommonLibModule { }
