import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';
import { Language, TranslationService } from 'angular-l10n';

import { User, Counter, Bill } from '../appModel';

import { AuthService } from '../auth/auth.service';
import { LoaderService } from '../shared/loader.service';
import { UserService } from '../user/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  @Language() lang;
  user = new User();
  topThree = new Array<Bill>();
  constructor(
    private auth: AuthService,
    private loader: LoaderService,
    private userService: UserService,
    private notify: SnotifyService,
    private translation: TranslationService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));

    this.getUser();
  }
  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  getUser() {
    this.loader.show();
    this.userService.getUser().subscribe(res => {
      this.auth.user = res;
      const temp = res.bills.filter(s => s.payment == null && s.settlementId == null);
      if (res.bills.length > 3) {
        var sorted = temp.sort(function (a, b) {
          return (a.amount < b.amount) ? 1 : ((b.amount < a.amount) ? -1 : 0);
        });
        this.topThree = sorted.slice(0, 3);
      } else {
        this.topThree = temp;
      }
      this.loader.hide();
    }, error => {
      this.notify.error(this.translation.translate('Snotify.ServerError'), this.translation.translate('Snotify.Error'));

      this.loader.hide();
    });
  }



}
