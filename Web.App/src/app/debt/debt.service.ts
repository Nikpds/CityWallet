import { Injectable, OnInit } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs/Rx';

import { environment } from '../../environments/environment';

import { Debt, User } from '../appModel';

import { AuthService } from '../auth/auth.service';

@Injectable()
export class DebtService implements OnInit {
  private debtUrl = environment.api + 'debt';

  private debtsToPaySubject$ = new BehaviorSubject<Debt[]>(new Array<Debt>());
  debtsToPay$ = this.debtsToPaySubject$.asObservable();

  get debtsToPay(): Debt[] {
    return this.debtsToPaySubject$.getValue();
  }

  set debtsToPay(value: Debt[]) {
    this.debtsToPaySubject$.next(value);
  }

  constructor(
    private auth: AuthService
  ) { }

  ngOnInit() {

  }

  getDebts(id: string): Observable<Array<Debt>> {
    return this.auth.get(this.debtUrl + "/debts/" + id)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }
}
