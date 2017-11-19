import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';
import { Language } from 'angular-l10n';

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
    private notify: SnotifyService
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
      if (res.bills.length > 4) {
        this.topThree = res.bills.sort(s => s.amount).slice(1, 4);
      } else {
        this.topThree = res.bills;
      }
      console.log(res);
      this.loader.hide();
    }, error => {
      this.notify.error(error, 'Σφάλμα ανάκτησης στοιχείων Χρήστη!');
      this.loader.hide();
    });
  }



}
