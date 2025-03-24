import { Component } from '@angular/core';

@Component({
  selector: 'app-customer-transaction-request',
  templateUrl: './customer-transaction-request.component.html',
  styleUrl: './customer-transaction-request.component.css'
})
export class CustomerTransactionRequestComponent {
  requests = [
    { id: 1, title: 'Activity Request 1', description: 'This is the first activity request.' },
    { id: 2, title: 'Activity Request 2', description: 'This is the second activity request.' },
    // Add more requests as needed
  ];
}
