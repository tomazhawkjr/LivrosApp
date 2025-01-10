import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LoginProvider } from 'src/app/providers/login.provider';
import { AuthService } from 'src/app/services/auth.storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  constructor(
    private readonly loginProvider: LoginProvider,    
    private readonly authStorage: AuthService,
    private readonly spinner: NgxSpinnerService,
    private readonly notification: ToastrService,
    private readonly router: Router,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {

    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }  

  onSubmit(): void {
    if (this.loginForm.valid) {

      this.spinner.show();

      const { username, password } = this.loginForm.value;
      this.loginProvider.Login(username, password).subscribe(
        {
          next: (v) => {
            this.authStorage.setToken(v.data);
            this.router.navigate(['/']);
          },
          error: (e) => {
            this.spinner.hide();
            this.notification.error(e.message ?? 'Ocorreu algum erro')
          },
          complete: () => {
            this.spinner.hide();
          }
        }
      );

    }
  }

}
