import { Validators } from "@angular/forms";

export const FullNameValidation = [
  Validators.required,
  Validators.minLength(3),
]

export const EmailValidation = [
  Validators.required, 
  Validators.email
]

export const PasswordValidation = [
  Validators.required,
  Validators.minLength(8),
  Validators.maxLength(50),
]