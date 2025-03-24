import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { NgToastService } from 'ng-angular-popup';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-bank-account',
  templateUrl: './add-bank-account.component.html',
  styleUrl: './add-bank-account.component.css'
})
export class AddBankAccountComponent implements OnInit{
  userName: string | null;
  createForm!: FormGroup;

  constructor(private api:ApiService, private toast: NgToastService){
    this.userName = this.api.getUsernameFromToken();
    this.createForm = new FormGroup({
      accountNumber: new FormControl([''], Validators.required)
    })
  }


  ngOnInit(): void {
    
  }

  onCreate(): void {
    if (this.createForm.valid) {
      const formData = this.createForm.value;
      formData.userName = this.userName;
      this.api.createAccount(formData).subscribe({
        next: (res) => {
          console.log('Data successfully posted to the database', res);
          this.toast.success("Create Account Number Successfully","SUCCESS", 5000);
          this.createForm.reset();
        },
        error: (err) => {
          console.error(err);
          this.toast.danger(err.message, "ERROR", 10000);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }

}
