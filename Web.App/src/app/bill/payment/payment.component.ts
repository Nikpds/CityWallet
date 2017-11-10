import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Router } from '@angular/router'
import { Observable, BehaviorSubject } from 'rxjs/Rx';
import { SnotifyService } from 'ng-snotify';
import { Language } from 'angular-l10n';

import { Bill, User, CreditCard } from '../../appModel';

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

  successAction = Observable.create(observer => {
    setTimeout(() => {
      observer.next({
        body: 'Still loading.....',
      });
    }, 2000);

    setTimeout(() => {
      observer.next({
        title: 'Success',
        body: 'Example. Data loaded!',
        config: {
          closeOnClick: true,
          timeout: 5000,
          showProgressBar: true
        }
      });
      observer.complete();
    }, 5000);
  });

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private auth: AuthService,
    private notify: SnotifyService,
    private router: Router
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
  }

  // paybills() {
  //   this.billService.validateCreditCard().subscribe(res => {
  //     this.notify.async('This will resolve with success', this.successAction);
  //   }, error => {

  //   });

  // }

  payBills() {
    this.loader.show();
    this.billService.paybills().subscribe(res => {
      this.billService.billsToPay = new Array<Bill>();
      this.loader.hide();
      this.notify.success('Η συναλλαγή ολοκληρώθηκε');
      this.router.navigate(['/payments']);
    }, error => {
      this.notify.error('Αδυναμία Πληρωμής');
      this.loader.hide();
    });
  }


}



