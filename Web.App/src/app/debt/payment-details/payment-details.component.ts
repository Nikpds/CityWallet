import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';

import { Debt, User,CreditCard } from '../../appModel';

import { DebtService } from '../debt.service';
import { LoaderService } from '../../shared/loader.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.sass']
})
export class PaymentDetailsComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  debtsForPay: Array<Debt>;
  user: User;
  step = 1;
  creditCard = new CreditCard();

  constructor(
    private debtService: DebtService,
    private loader: LoaderService,
    private auth: AuthService,
    private notify: SnotifyService
  ) { }


  previousStep() {
    this.step = this.step == 0 ? this.step : this.step - 1;
  }

  nextStep() {
    this.step = this.step == 2 ? this.step : this.step + 1;
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
    this.subscriptions.push(this.debtService.debtsToPay$
      .subscribe((res) => this.debtsForPay = res));
  }

}
