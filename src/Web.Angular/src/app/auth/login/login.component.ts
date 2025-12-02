import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EmailValidation, PasswordValidation } from '../../common/validations';
import { AuthService } from '../../common/auth.service';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: '../../common/styles/auth.scss'
})
export class LoginComponent implements OnInit {

    loginForm!: FormGroup;
    loginError!: string | undefined;

    constructor(
      private formBuilder: FormBuilder, 
      private security: AuthService,
      private router: Router
    ) { }
    
    ngOnInit (): void {
      this.buildLoginForm();
    }

    buildLoginForm() {
      this.loginForm = this.formBuilder.group({
        email: ['', EmailValidation],
        password: ['', PasswordValidation]
      });
    }

    login(submitedForm: FormGroup) {
      this.security.login(
        // submitedForm.value.email, 
        // submitedForm.value.password,

        // "rider1@rides.com",
        // "MyP455w0rd!",

        "driver1@rides.com",
        "MyP455w0rd!",
      ).pipe(
        tap((res) => {
          console.log('Login successful');
          this.router.navigate(['/home']);
        }
      )).subscribe({
        error: (err) => {
          console.error('Login error', err);
          this.loginError = 'Invalid email or password.';
        }
      });
    }
}
