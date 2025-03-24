import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountManagementComponent } from './account-management.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: AccountManagementComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class AccountManagementModule { }
 