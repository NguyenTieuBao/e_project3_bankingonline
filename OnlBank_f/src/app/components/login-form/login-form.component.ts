import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

  public loginForm!: FormGroup;

  constructor(
    private apiService: ApiService,
    private fb: FormBuilder,
    private toast: NgToastService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onLogin() {
    if (this.loginForm.valid) {
      const loginObj = {
        username: this.loginForm.value.username,
        password: this.loginForm.value.password
      };
      // console.log('Login data:', loginObj); // In ra dữ liệu gửi đi

      this.apiService.loginUser(loginObj).subscribe({
        next: (res) => {
          console.log('Response:', res);
          this.loginForm.reset();
          this.apiService.storeToken(res.token);
          this.toast.success(res.message, "SUCCESS", 5000);
          this.router.navigate(['customer']);
        },
        error: (err) => {
          console.log('Full error object:', err);

          if (err.error && err.error.Message) {
            this.toast.danger(err.error.Message, "WARNING", 5000);
            console.error('Error message:', err.error.Message);
          } else if (err.message) {
            this.toast.danger(err.message, "WARNING", 5000);
            console.error('Error message:', err.message);
          } else {
            this.toast.danger('Unknown error occurred', "WARNING", 5000);
            console.error('Unknown error:', err);
          }

          console.log("Error");
        }
      });
    } else {
      this.toast.warning('Please enter both username and password.', "WARNING", 5000);
    }
  }
}
