import { Component, OnInit, Input, OnDestroy, EventEmitter, Output } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Language } from 'angular-l10n';

import { CreditCard, Bill, User } from '../../appModel';

import { BillService } from '../bill.service';
import { AuthService } from '../../auth/auth.service';
@Component({
  selector: 'app-payment-overview',
  templateUrl: './payment-overview.component.html',
  styleUrls: ['./payment-overview.component.sass']
})
export class PaymentOverviewComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  @Output() setState: EventEmitter<number> = new EventEmitter();
  @Language() lang: string;

  bills: Array<Bill>;
  user: User;
  creditCard = new CreditCard();

  constructor(
    private billService: BillService,
    private auth: AuthService
  ) { }


  editCreditCard() {
    this.setState.emit(1);
  }

  ngOnInit() {
    this.subscriptions.push(this.billService.billsToPay$
      .subscribe((res) => this.bills = res));

    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));

    this.subscriptions.push(this.billService.creditCard$
      .subscribe((res) => this.creditCard = res));
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  billsSum() {
    return (this.bills.reduce(function (a, b) { return a + b.amount }, 0)).toFixed(2);
  }

}
