import { Component } from '@angular/core';
import { ApiService, RegisterDto } from '../../services/api.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent {
  registerDto: RegisterDto = {
    username: '',
    password: '',
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    role: 'user' 
  };
  passwordConfirm: string = '';
  message: string = '';

  constructor(private api: ApiService, private toast: NgToastService) { }

  register(): void {
    if (this.registerDto.password !== this.passwordConfirm) {
      this.message = 'Passwords do not match!';
      this.toast.warning("'Passwords do not match!", "WARNING", 5000);
      return;
    }
    this.api.registerUser(this.registerDto).subscribe({
      next: (response) => {
        this.message = response.message;
        this.toast.success(response.message, "SUCCESS", 5000);
      },
      error: (error) => {
        this.message = error.error.message || 'An error occurred during registration.';
        this.toast.danger(error.error.message, "ERROR", 5000);

      }
    });
  }
}
