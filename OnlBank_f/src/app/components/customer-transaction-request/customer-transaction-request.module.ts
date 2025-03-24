import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerTransactionRequestComponent } from './customer-transaction-request.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: CustomerTransactionRequestComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class CustomerTransactionRequestModule { }
 