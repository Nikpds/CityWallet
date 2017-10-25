import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Bill, User } from '../../appModel';
import { Router } from '@angular/router'

import { BillService } from '../bill.service';
@Component({
  selector: 'app-bill-to-pay',
  templateUrl: './bill-to-pay.component.html',
  styleUrls: ['./bill-to-pay.component.sass']
})
export class BillToPayComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  bills: Array<Bill>;

  constructor(
    private billService: BillService,
    private router: Router
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.billService.billsToPay$
      .subscribe((res) => this.bills = res));

    if (this.bills.length == 0) {
     // this.router.navigate(['/bills']);
    }
  }

  billsSum() {
    return this.bills.reduce(function (a, b) { return a + b.amount }, 0);
  }

  removebillFromPay(i: number) {
    this.bills.splice(i, 1);
    if (this.bills.length == 0) {
      this.router.navigate(['/bills']);
    }
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }
}
