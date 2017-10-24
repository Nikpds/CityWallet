import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';

import { Bill, User } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.sass']
})
export class BillListComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  billsForPay: Array<Bill>;
  user: User;
  temp = true;
  bills = new Array<Bill>();
  billType = 0;

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private auth: AuthService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
    this.subscriptions.push(this.billService.billsToPay$
      .subscribe((res) => this.billsForPay = res));

    this.getbills();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  getbills() {
    this.loader.show();
    this.billService.getbills(this.user.id).subscribe((res) => {
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
