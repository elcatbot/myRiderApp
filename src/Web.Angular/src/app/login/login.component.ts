import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EmailValidation, PasswordValidation } from '../common/validations';
import { SecurityService } from '../common/security.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

    loginForm!: FormGroup;

    constructor(private formBuilder: FormBuilder, private security: SecurityService) { }
    
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
      this.security.login(submitedForm.value.email, submitedForm.value.password)
        .subscribe({
          next: (res) => {
            console.log('Login successful', res);
          },
          error: (err) => {
            console.error('Login failed', err);
          }
        });
    }
}
