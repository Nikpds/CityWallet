import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Router } from '@angular/router'
import { Observable, BehaviorSubject } from 'rxjs/Rx';
import { SnotifyService } from 'ng-snotify';
import { Language, TranslationService } from 'angular-l10n';

import { Bill, User, CreditCard, NavigationC } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.sass']
})
export class PaymentComponent implements OnInit, OnDestroy {
  @Language() lang: string;
  private subscriptions = new Array<Subscription>();
  billsForPay: Array<Bill>;
  user: User;
  step = 0;
  creditCard = new CreditCard();
  navbar = new Array<NavigationC>();

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private auth: AuthService,
    private notify: SnotifyService,
    private router: Router,
    private translation: TranslationService
  ) { }

  previousStep() {
    this.step = this.step === 0 ? this.step : this.step - 1;
  }

  nextStep() {
    this.step = this.step === 2 ? this.step : this.step + 1;
  }

  ediCreditCard(step: number) {
    this.step = step;
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  cancel() {
    this.billService.billsToPay = [];
    this.router.navigate(['/bills']);
  }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
    this.subscriptions.push(this.billService.billsToPay$
      .subscribe((res) => this.billsForPay = res));

    this.navbar = [{
      icon: "fa-home", title: "App.Home", navigateTo: () => this.goHome(), isActive: false
    }, {
      icon: "fa-file-text-o", title: "Home.Bills", navigateTo: () => this.goBills(), isActive: false
    }, {
      icon: "fa-credit-card-alt", title: "Payments.PaymentSteps", navigateTo: () => null, isActive: true
    }];
  }

  goHome() {
    this.router.navigate(['/']);
  }

  goBills() {
    this.router.navigate(['/bills'])
  }

  payBills() {
    this.loader.show();
    this.billService.paybills().subscribe(res => {
      this.billService.billsToPay = new Array<Bill>();
      this.loader.hide();
      this.notify.success(this.translation.translate('Snotify.PaymentComplete'), this.translation.translate('Snotify.Success'));
      this.router.navigate(['/payments']);
    }, error => {
      this.notify.error(this.translation.translate('Snotify.PaymentError'), this.translation.translate('Snotify.Error'));
      this.loader.hide();
    });
  }


}



