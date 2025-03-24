import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StaffManagerAccountManagementComponent } from './staff-manager-account-management.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: StaffManagerAccountManagementComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class StaffManagerAccountManagementModule { }
 