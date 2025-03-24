import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BankAccountComponent } from './bank-account.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: BankAccountComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class BankAccountModule { }
