import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerRevenueComponent } from './customer-revenue.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: CustomerRevenueComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class CustomerRevenueModule { }
 