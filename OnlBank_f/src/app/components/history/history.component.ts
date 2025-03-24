import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {

  username: string | null;
  allAccount: any[] = [];
  historyTransfer: any[] = [];
  paginatedDataTransfer: any[] = [];
  pageSize = 10;
  pageIndex = 0;
  accountNumber: string | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private api: ApiService) {
    this.username = this.api.getUsernameFromToken();
  }

  ngOnInit(): void {
    this.loadAllAccount();
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.updatePaginatedDataTransfer();
  }

  updatePaginatedDataTransfer(): void {
    const startIndex = this.pageIndex * this.pageSize;
    let endIndex = startIndex + this.pageSize;
    if (endIndex > this.historyTransfer.length) {
      endIndex = this.historyTransfer.length;
    }
    this.paginatedDataTransfer = this.historyTransfer.slice(startIndex, endIndex);
  }

  loadAllAccount(): void {
    this.api.getAccountByUsername(this.username!).subscribe({
      next: (accounts) => {
        this.allAccount = accounts;
      },
      error: (error) => {
        console.error('Lỗi khi tải tài khoản', error);
      }

    });
  }

  loadHistoryTransfer(): void {
    this.api.getHistoryTransfer(this.accountNumber!).subscribe({
      next: (historyTransfer) => {
        this.historyTransfer = historyTransfer;
        this.updatePaginatedDataTransfer();
      },
      error: (error) => {
        console.error('Lỗi khi tải lịch sử giao dịch', error);
      }
    });
  }


}

