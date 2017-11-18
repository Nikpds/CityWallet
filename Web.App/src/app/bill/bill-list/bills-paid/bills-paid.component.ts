import { Component, OnInit, } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { Language } from 'angular-l10n';

import { Bill, User, Config } from '../../../appModel';

import { BillService } from '../../bill.service';
import { LoaderService } from '../../../shared/loader.service';
import { PaginationService } from '../../../shared/pagination.service';
@Component({
  selector: 'app-bills-paid',
  templateUrl: './bills-paid.component.html',
  styleUrls: ['./bills-paid.component.sass']
})
export class BillsPaidComponent implements OnInit {
  bills = new Array<Bill>();
  selectedBill: Bill;
  config = new Config();
  searchText: string;
  filterText: string;
  @Language() lang;

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private notify: SnotifyService,
    private srv: PaginationService
  ) { }

  ngOnInit() {
    this.getPaidBills();
  }

  print() {
    window.print();
  }

  Search(t: string) {
    this.searchText = t.trim();
    this.filterText = t.trim();
  }

  getPaidBills() {
    this.loader.show();
    this.billService.getPaidBills().subscribe((res) => {
      this.bills = res;
      this.initConfig(res);
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  initConfig(data: any) {
    this.config.currentPage = 1;
    this.config.pageSize = 10;
    this.config.data = data;
    this.config.totalPages = this.getTotalPages(data.length, this.config.pageSize);
    this.config.pages = this.srv.getPages(this.config.totalPages, this.config.currentPage);
  }

  getTotalPages(length: number, size: number) {
    let pageSize = Math.floor(length / size);
    if (pageSize != length / size) {
      return pageSize + 1
    }
    return pageSize
  }
  
  TotalDebt() {
    return (this.bills.reduce(function (a, b) { return a + b.amount }, 0)).toFixed(2);
  }
}
