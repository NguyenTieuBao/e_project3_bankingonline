import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-customer-profile',
  templateUrl: './customer-profile.component.html',
  styleUrl: './customer-profile.component.css'
})
export class CustomerProfileComponent implements OnInit{
  fullname: string | null;
  username: string | null;
  profileData: any = {};
  constructor(private api: ApiService) {
    this.fullname = this.api.getFullNameFromToken();
    this.username = this.api.getUsernameFromToken();
  }
  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.api.getProfileUser(this.username!).subscribe(
      (data) => {
        this.profileData = data;
      },
      (error) => {
        console.error('Error loading user profile:', error);
      }
    );
  }

}
