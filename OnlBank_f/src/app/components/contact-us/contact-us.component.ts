import { Component, OnInit } from '@angular/core';
import { ApiService, RequestDto } from '../../services/api.service';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrl: './contact-us.component.css'
})
export class ContactUsComponent implements OnInit{
  request: RequestDto = {
    username: '',
    requestType: '',
    requestDate: new Date(),
    status: ''
  };
  requests: RequestDto[] = [];
  username: string = '';

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.username = this.api.getUsernameFromToken() || '';
    this.loadRequestsByUsername();
  }

  loadRequestsByUsername(): void {
    if (this.username) {
      this.api.getRequestsByUsername(this.username).subscribe((data: RequestDto[]) => {
        this.requests = data;
      });
    }
  }

  submitRequest(): void {
    this.request.username = this.username;
    this.api.postRequest(this.request).subscribe((data: RequestDto) => {
      console.log('Request submitted', data);
      this.loadRequestsByUsername(); // Reload requests after submission
    });
  }
}
