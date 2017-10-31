import { Component, OnInit, } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { Language } from 'angular-l10n';

import { Bill, User } from '../../../appModel';

import { BillService } from '../../bill.service';
import { LoaderService } from '../../../shared/loader.service';
@Component({
  selector: 'app-bills-paid',
  templateUrl: './bills-paid.component.html',
  styleUrls: ['./bills-paid.component.sass']
})
export class BillsPaidComponent implements OnInit {
  bills = new Array<Bill>();
  selectedBill: Bill;
  @Language() lang;

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.getPaidBills();
  }

  selectBill(i: number) {
    if (this.selectedBill && this.selectedBill.id == this.bills[i].id) {
      this.selectedBill = null;
      return;
    }
    this.selectedBill = this.bills[i];
  }

  getPaidBills() {
    this.loader.show();
    this.billService.getPaidBills().subscribe((res) => {
      this.bills = res;
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  checkSelected(id: string) {
    if (!this.selectedBill) { return false }
    return this.selectedBill.id == id;
  }
}
