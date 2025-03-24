import { Component } from '@angular/core';

@Component({
  selector: 'app-test-table-row-selected',
  templateUrl: './test-table-row-selected.component.html',
  styleUrl: './test-table-row-selected.component.css'
})
export class TestTableRowSelectedComponent {
  selectedRow: any = null;

  rows = [
    { id: 1, name: 'John Doe', age: 30, job: 'Engineer' },
    { id: 2, name: 'Jane Smith', age: 25, job: 'Designer' },
    { id: 3, name: 'Sam Johnson', age: 35, job: 'Developer' },
    // Add more rows as needed
  ];

  onSelectRow(row: any) {
    this.selectedRow = row;
  }
}
