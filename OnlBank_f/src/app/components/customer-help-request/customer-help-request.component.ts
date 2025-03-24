import { Component } from '@angular/core';

@Component({
  selector: 'app-customer-help-request',
  templateUrl: './customer-help-request.component.html',
  styleUrl: './customer-help-request.component.css'
})
export class CustomerHelpRequestComponent {
  requests = [
    { id: 1, title: 'Help Request 1', description: 'This is the first help request.' },
    { id: 2, title: 'Help Request 2', description: 'This is the second help request.' },
    // Add more requests as needed
  ];
}
