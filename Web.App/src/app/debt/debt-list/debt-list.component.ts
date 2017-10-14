import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { Debt, User } from '../../appModel';

import { DebtService } from '../debt.service';
import { LoaderService } from '../../shared/loader.service';
import { AuthService } from '../../auth/auth.service';
import { NotificationService } from '../../shared/notification.service'
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-debt-list',
  templateUrl: './debt-list.component.html',
  styleUrls: ['./debt-list.component.sass']
})
export class DebtListComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  user: User;

  debts = new Array<Debt>();

  constructor(
    private debtService: DebtService,
    private loader: LoaderService,
    private snotifyService: SnotifyService,
    private auth: AuthService,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
      
    this.getDebts();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  getDebts() {
    // this.loader.show();
    this.debtService.getDebts(this.user.id).subscribe((res) => {
      this.debts = res;
      console.log(res);
      this.snotifyService.success('Example body content', {
        timeout: 2000,
        showProgressBar: false,
        closeOnClick: false,
        pauseOnHover: true
      });
      // this.loader.hide();
    }, error => {

      //  this.loader.hide();
    });
  }
}
