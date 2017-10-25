import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';

import { User, Counter } from '../appModel';

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
  user = new User();
  counters = new Counter();
  constructor(
    private auth: AuthService,
    private loader: LoaderService,
    private userService: UserService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));

    this.subscriptions.push(this.userService.counters$
      .subscribe((counters) => this.counters = counters));
      
    this.getUser();
  }
  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  getUser() {
    this.loader.show();
    this.userService.getUser().subscribe(res => {
      this.auth.user = res;
      this.loader.hide();
    }, error => {
      this.notify.error(error, 'Σφάλμα ανάκτησης στοιχείων Χρήστη!');
      this.loader.hide();
    });
  }
}
