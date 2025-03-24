import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { NgToastService } from 'ng-angular-popup';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  userName: string | null;
  changedPass!: FormGroup;
  constructor(private api: ApiService, private toast: NgToastService){
    this.userName = this.api.getUsernameFromToken();
    this.changedPass = new FormGroup({
      passwordCurrent: new FormControl([''], Validators.required),
      passwordNew: new FormControl([''], Validators.required),
      passwordVerify: new FormControl([''], Validators.required),
    })
  }
  ngOnInit(): void {
    
  }

  onClick()
  {
    if(this.changedPass.valid)
    {
      const formData = this.changedPass.value;
      if (formData.passwordNew !== formData.passwordVerify) {
        this.toast.warning("Passwords do not match", "WARNING", 5000);
        return;
      }
      formData.userName = this.userName;
      this.api.changedPasswordUser(formData).subscribe({
        next: (res) => {
          console.log('Data successfully posted to the database', res);
          this.toast.success("Changed Password Successfully","SUCCESS", 5000);
          this.changedPass.reset();
        },
        error: (err) => {
          console.error(err);
          this.toast.danger("Current password not valid, try again", "ERROR", 10000);
        }
      })
    } else {
      this.toast.warning("Form is invalid", "WARNING", 5000);
      console.log('Form is invalid');
    }
  }

}
