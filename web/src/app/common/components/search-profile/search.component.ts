import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { PayrollService } from 'src/app/pages/payroll/services/payroll.service';
import { SwalClass } from '../../classes/swal.class';
import { ProfileService } from '../../services/profile.service';


@Component({
  selector: 'app-search-profile',
  templateUrl: './search-profile.component.html',
  styles: [
  ]
})
export class SearchComponent implements OnInit {
  @Input() public withUsernameField!: boolean;
  @Input() public endPointResource!: string;
  @Input() public cardTitle:string = '';
  @Output() public searchCompleted: EventEmitter<any>;
  public searching: boolean;
  public frmGroup: FormGroup;

  constructor(private srvProfile: ProfileService, private swal: SwalClass) {
    this.searchCompleted = new EventEmitter();
    this.searching = false;
    this.frmGroup = new FormGroup({
      workday: new FormControl(''),
      username: new FormControl(''),
      name: new FormControl('')
    });
  }

  ngOnInit(): void {
  }

  formSubmit(): void {
    this.searching = true;
    this.srvProfile.getProfiles(this.frmGroup.value, this.endPointResource ?? '').subscribe({

      next: (result: any) => {
        if (!result.isCorrect)
          this.swal.showAlert('Attention', result.message, 'warning');

        this.searchCompleted.emit(result.data as any[]);
        this.searching = false;
      },
      error: (err: HttpErrorResponse) => {
        this.searching = false;
        throw err;
      }
    });
  }
}
