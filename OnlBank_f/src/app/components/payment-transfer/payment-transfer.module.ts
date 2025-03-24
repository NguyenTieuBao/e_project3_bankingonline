import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentTransferComponent } from './payment-transfer.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: PaymentTransferComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class PaymentTransferModule { }
