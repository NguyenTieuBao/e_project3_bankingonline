import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HistoryComponent } from './history.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: HistoryComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class HistoryModule { }
