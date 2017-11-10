import { Component, OnInit } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { PaginationInstance } from '../../../../../node_modules/ngx-pagination';
import { Language } from 'angular-l10n';

import { Bill, Config } from '../../../appModel';

import { BillService } from '../../bill.service';
import { LoaderService } from '../../../shared/loader.service';
import { PaginationService } from '../../../shared/pagination.service';

@Component({
  selector: 'app-bills-settled',
  templateUrl: './bills-settled.component.html',
  styleUrls: ['./bills-settled.component.sass']
})
export class BillsSettledComponent implements OnInit {
  bills = new Array<Bill>();
  billsForPay: Array<Bill>;
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
    this.getSettledBills();
  }

  getSettledBills() {
    this.loader.show();
    this.billService.getSettledBills().subscribe((res) => {
      this.bills = res;
      this.initConfig(res);;
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  checkFallDue(d: Date) {
    let dueDate = new Date(d);
    return new Date() > dueDate;
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

  print() {
    window.print();
  }

  Search(t: string) {
    this.searchText = t.trim();
    this.filterText = t.trim();
  }

}
