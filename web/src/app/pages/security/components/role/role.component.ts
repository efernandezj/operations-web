import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SwalClass } from 'src/app/common/classes/swal.class';
import { RoleService } from '../../services/role.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  public loading: boolean;
  public initData: any;
  
  constructor(private swal: SwalClass, private srvRole: RoleService) { 
    this.loading = false;
    this.initData = {};
  }

  ngOnInit(): void {
    this.srvRole.getInitData().subscribe({
      next: (result: any) => {
        if (result.isCorrect) {
          console.log(result);
          this.initData = result.data as any;
        } else {
          this.swal.showAlert('Attention!', result.message, 'warning')
        }
        this.loading = false;
      },

      error: (err: HttpErrorResponse) => {
        this.loading = false;
        throw err;
      }
    });
  }
}
