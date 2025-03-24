import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-main-navigation',
  templateUrl: './main-navigation.component.html',
  styleUrl: './main-navigation.component.css'
})
export class MainNavigationComponent implements OnInit{

  constructor(private http: ApiService) {
    
  }

  ngOnInit(): void {
    
  }

  logout()
  {
    this.http.signOutUser();
  }

}
