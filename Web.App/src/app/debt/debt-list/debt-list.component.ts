import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';

import { Debt, User } from '../../appModel';

import { DebtService } from '../debt.service';
import { LoaderService } from '../../shared/loader.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-debt-list',
  templateUrl: './debt-list.component.html',
  styleUrls: ['./debt-list.component.sass']
})
export class DebtListComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  debtsForPay: Array<Debt>;
  user: User;

  debts = new Array<Debt>();

  constructor(
    private debtService: DebtService,
    private loader: LoaderService,
    private auth: AuthService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
    this.subscriptions.push(this.debtService.debtsToPay$
      .subscribe((res) => this.debtsForPay = res));

    this.getDebts();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  getDebts() {
    this.loader.show();
    this.debtService.getDebts(this.user.id).subscribe((res) => {
      this.debts = res;
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  addRemoveDebt(i: number, id: string) {
    var index = this.debtsForPay.findIndex(x => x.id == id);
    if (index > -1) {
      this.debtsForPay.splice(index, 1);
      console.log(this.debtsForPay);
      return;
    }
    this.debtsForPay.push(this.debts[i]);
    console.log(this.debtsForPay);
  }

  isOnPayList(id: string) {
    return this.debtsForPay.findIndex(x => x.id == id) > -1;
  }
}
