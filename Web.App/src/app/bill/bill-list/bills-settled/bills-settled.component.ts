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
      // this.initConfig(res);
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  checkFallDue(d: Date) {
    let dueDate = new Date(d);
    return new Date() > dueDate;
  }

}
