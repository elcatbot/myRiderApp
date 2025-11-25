import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EmailValidation, PasswordValidation } from '../../common/validations';
import { SecurityService } from '../../common/security.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: '../../common/styles/auth.scss'
})
export class LoginComponent implements OnInit {

    loginForm!: FormGroup;

    constructor(
      private formBuilder: FormBuilder, 
      private security: SecurityService,
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
        // submitedForm.value.password
        "rider1@rides.com",
        "MyP455w0rd!",
      )
        .subscribe({
          next: (res) => {
            console.log('Login successful');
            this.router.navigate(['/home']);
          },
          error: (err) => {
            console.error('Login failed', err);
          }
        });
    }
}
