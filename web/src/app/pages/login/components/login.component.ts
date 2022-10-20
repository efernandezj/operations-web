import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { SwalClass } from 'src/app/common/classes/swal.class';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public isProcessing: boolean;
  public frmGroup: FormGroup

  constructor(private srvLogin: AuthService, private router: Router, private swal: SwalClass) {
    this.isProcessing = false;
    this.frmGroup = new FormGroup({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', Validators.required)
    });
  }

  ngOnInit(): void {
    if (this.srvLogin.isAuthenticated()) {
      this.router.navigate(['/home']);
    }
  }

  public formSubmit(): void {
    this.isProcessing = true;

    this.srvLogin.postLogin(this.frmGroup.value).subscribe({
      
      next: (result: any) => {
        if (result.isCorrect) {
          this.srvLogin.setSession(result.authenticatedResponse);
          this.router.navigate(['/home']);         
        } else {
          this.swal.showAlert('Attention', result.message, 'error');
          this.frmGroup.get("")?.setValue('');
        }
        this.isProcessing = false;
      },
      error: (err) => {
        this.isProcessing = false;
        throw err;
      }
    });
  }


}
