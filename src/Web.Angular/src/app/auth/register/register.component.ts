import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../common/auth.service';
import { EmailValidation, FullNameValidation, PasswordValidation } from '../../common/validations';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: '../../common/styles/auth.scss'
})
export class RegisterComponent {
  registerForm!: FormGroup;

    constructor(
      private formBuilder: FormBuilder, 
      private security: AuthService,
      private router: Router
    ) { }
    
    ngOnInit (): void {
      this.buildRegistrationForm();
    }

    buildRegistrationForm() {
      this.registerForm = this.formBuilder.group({
        fullName: ['', FullNameValidation],
        role: ['', Validators.required],
        email: ['', EmailValidation],
        password: ['', PasswordValidation]
      });
    }

    register(submitedForm: FormGroup) {
      this.security.register(
        // submitedForm.value.email, 
        // submitedForm.value.fullName, 
        // submitedForm.value.password
        // submitedForm.value.role, 
 
        // "riderOne",
        // "rider",
        // "rider1@rides.com",
        // "MyP455w0rd!",

        "DriverOne",
        "driver",
        "driver1@rides.com",
        "MyP455w0rd!",
      )
        .subscribe({
          next: (res) => {
            console.log('User registered successfully', res);
            this.router.navigate(['/login']);
          },
          error: (err) => {
            console.error('Registration failed', err);
          }
        });
    }
}
