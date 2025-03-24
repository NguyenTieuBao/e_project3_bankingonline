import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerHelpRequestComponent } from './customer-help-request.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: CustomerHelpRequestComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class CustomerHelpRequestModule { }
 