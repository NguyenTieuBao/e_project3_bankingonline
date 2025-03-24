import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerProfileComponent } from './customer-profile.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: CustomerProfileComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class CustomerProfileModule { }
