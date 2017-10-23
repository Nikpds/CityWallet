import { Injectable, OnInit } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs/Rx';

import { environment } from '../../environments/environment';

import { Bill, User, CreditCard } from '../appModel';

import { AuthService } from '../auth/auth.service';

@Injectable()
export class BillService implements OnInit {
  private billUrl = environment.api + 'bill';
  private payUrl = environment.api + 'payment';

  private billsToPaySubject$ = new BehaviorSubject<Bill[]>(new Array<Bill>());
  billsToPay$ = this.billsToPaySubject$.asObservable();

  private creditCardSubject$ = new BehaviorSubject<CreditCard>(new CreditCard());
  creditCard$ = this.creditCardSubject$.asObservable();

  get billsToPay(): Bill[] {
    return this.billsToPaySubject$.getValue();
  }

  set billsToPay(value: Bill[]) {
    this.billsToPaySubject$.next(value);
  }

  get creditCard(): CreditCard {
    return this.creditCardSubject$.getValue();
  }

  set creditCard(value: CreditCard) {
    this.creditCardSubject$.next(value);
  }

  constructor(
    private auth: AuthService
  ) { }

  ngOnInit() {

  }

  getbills(id: string): Observable<Array<Bill>> {
    return this.auth.get(this.billUrl + "/bills/" + id)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

  paybills() {
    return this.auth.post(this.payUrl + "/paybills", this.billsToPay)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

  validateCreditCard() {
    return this.auth.post(this.payUrl + "/creditcard", this.creditCard)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

  getPayments(id: string) {
    return this.auth.get(this.payUrl + "/payments/" + id)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }
}
