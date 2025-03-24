import { Component } from '@angular/core';

@Component({
  selector: 'app-admin-profile',
  templateUrl: './admin-profile.component.html',
  styleUrl: './admin-profile.component.css'
})
export class AdminProfileComponent {
  profileImageUrl = './assets/img/bank.png';
  firstName = 'John';
  lastName = 'Doe';
  email = 'john.doe@example.com';
  id = '12345';
  phoneNumber = '123-456-7890';

  onImageChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = e => this.profileImageUrl = reader.result as string;
      reader.readAsDataURL(file);
    }
  }

  updateProfile() {
    const updatedProfile = {
      profileImageUrl: this.profileImageUrl,
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      id: this.id,
      phoneNumber: this.phoneNumber
    };
    console.log('Profile updated:', updatedProfile);
    alert('Profile updated successfully!');
  }
}
