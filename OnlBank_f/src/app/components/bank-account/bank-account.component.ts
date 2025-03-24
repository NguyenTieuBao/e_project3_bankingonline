import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-bank-account',
  templateUrl: './bank-account.component.html',
  styleUrl: './bank-account.component.css'
})
export class BankAccountComponent implements OnInit {
  fullname: string | null;
  username: string | null;
  allAccount: any[] = [];
  constructor(private api:ApiService)
  {
    this.fullname = this.api.getFullNameFromToken();
    this.username = this.api.getUsernameFromToken();
  }

  colors: string[] = [];

  ngOnInit(): void {
    // this.colors = this.allAccount.map(() => this.getRandomColor());
    this.loadAllAccount();
  }

  getRandomColor(): string {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  }

  loadAllAccount(): void {
    this.api.getAccountByUsername(this.username!).subscribe({
      next: (account) => {
        this.allAccount = account; 
      },
      error: (error) => {
        console.error('error', error);
      }
    });
  }
}
