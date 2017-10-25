import { Component, OnInit, OnDestroy  } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';

import { Bill, User } from '../../../appModel';

import { BillService } from '../../bill.service';
import { LoaderService } from '../../../shared/loader.service';
@Component({
  selector: 'app-bills-unpaid',
  templateUrl: './bills-unpaid.component.html',
  styleUrls: ['./bills-unpaid.component.sass']
})
export class BillsUnpaidComponent implements OnInit {
  private subscriptions = new Array<Subscription>();
  bills = new Array<Bill>();
  billsForPay: Array<Bill>;

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.billService.billsToPay$
      .subscribe((res) => this.billsForPay = res));
    this.getUnPaidBills();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }


  getUnPaidBills() {
    this.loader.show();
    this.billService.getbills().subscribe((res) => {
      this.bills = res;
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  addRemovebill(i: number, id: string) {
    var index = this.billsForPay.findIndex(x => x.id == id);
    if (index > -1) {
      this.billsForPay.splice(index, 1);
      return;
    }
    this.billsForPay.push(this.bills[i]);
  }

  isOnPayList(id: string) {
    return this.billsForPay.findIndex(x => x.id == id) > -1;
  }

  checkFallDue(d: Date) {
    var dueDate = new Date(d);
    return new Date() > dueDate;
  }

}
