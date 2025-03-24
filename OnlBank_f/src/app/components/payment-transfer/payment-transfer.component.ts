import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-payment-transfer',
  templateUrl: './payment-transfer.component.html',
  styleUrls: ['./payment-transfer.component.css']
})
export class PaymentTransferComponent implements OnInit {
  username: string | null;
  allAccount: any[] = [];
  fullName: string | null = null;
  transferForm!: FormGroup;

  constructor(private api: ApiService, private toast: NgToastService) {
    this.username = this.api.getUsernameFromToken();
    this.transferForm = new FormGroup({
      fromAccount: new FormControl("", [Validators.required]),
      toAccount: new FormControl("", [Validators.required]),
      amount: new FormControl("", [Validators.required]),
      transferDate: new FormControl(new Date().toISOString(), [Validators.required]),
      status: new FormControl("Pending", [Validators.required])
    });
  }

  ngOnInit(): void {
    this.loadAllAccount();
  }

  loadAllAccount(): void {
    this.api.getAccountByUsername(this.username!).subscribe({
      next: (account) => {
        this.allAccount = account;
      },
      error: (error) => {
        console.error('Error', error);
      }
    });
  }

  getFullName(): void {
    const accountNumber = this.transferForm.get('toAccount')?.value;
    if (accountNumber) {
      this.api.getSearchAccount(accountNumber).subscribe({
        next: (response) => {
          this.fullName = response.fullName;
        },
        error: (error) => {
          console.error('Error fetching account', error);
          this.fullName = null;
        }
      });
    }
  }

  onTransfer(): void {
    if (this.transferForm.valid) {
      const formData = this.transferForm.value;
      this.api.transfer(formData).subscribe({
        next: (response) => {
          console.log('Data successfully posted to the database', response);
          this.toast.success(response.message,"SUCCESS", 5000);
          this.transferForm.reset();
        },
        error: (error) => {
          console.error('Error posting data to the database', error);
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
