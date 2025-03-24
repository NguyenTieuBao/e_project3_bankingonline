import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-home-section',
  templateUrl: './home-section.component.html',
  styleUrls: ['./home-section.component.css']
})
export class HomeSectionComponent implements OnInit {
  fullname: string | null;
  username: string | null;
  accountCount: number = 0;
  total: number = 0;
  history: any[] = [];
  paginatedDataTransfer: any[] = [];
  pageSize = 5;
  pageIndex = 0;

  constructor(private api: ApiService) {
    this.fullname = this.api.getFullNameFromToken();
    this.username = this.api.getUsernameFromToken();
  }

  ngOnInit(): void {
    this.loadHistory();
    this.loadAccountCount();
    this.loadTotalCurrentBalance();
  }

  loadHistory(): void {
    this.api.getHistoryTrans(this.username!).subscribe({
      next: (history) => {
        this.history = history;
        this.updatePaginatedDataTransfer();
      },
      error: (error) => {
        console.error('Lỗi khi tải lịch sử giao dịch', error);
      }
    });
  }

  loadAccountCount(): void {
    this.api.getAccountCountByUsername(this.username!).subscribe(
      (count) => {
        this.accountCount = count;
      },
      (error) => {
        console.error('Lỗi khi tải số lượng tài khoản:', error);
      }
    );
  }

  loadTotalCurrentBalance(): void {
    this.api.getTotalCurrentBalance(this.username!).subscribe(
      (res) => {
        this.total = res;
      },
      (error) => {
        console.error('Lỗi khi tải số lượng tài khoản:', error);
      }
    );
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.updatePaginatedDataTransfer();
  }

  updatePaginatedDataTransfer(): void {
    const startIndex = this.pageIndex * this.pageSize;
    let endIndex = startIndex + this.pageSize;
    if (endIndex > this.history.length) {
      endIndex = this.history.length;
    }
    this.paginatedDataTransfer = this.history.slice(startIndex, endIndex);
  }
}

