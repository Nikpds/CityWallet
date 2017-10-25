import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';

import { User, Counter } from './appModel';

import { AuthService } from './auth/auth.service';
import { UserService } from './user/user.service';
import { LoaderService } from './shared/loader.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  user: User;
  counters = new Counter();
  constructor(
    private userService: UserService,
    private auth: AuthService,
    private notify: SnotifyService,
    private loader: LoaderService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
    this.subscriptions.push(this.userService.counters$
      .subscribe((counters) => this.counters = counters));
      
      this.getCounters();
  }
  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  logout() {
    this.auth.logOut();
  }

  getCounters() {
    this.loader.show();
    this.userService.getCounters().subscribe(res => {
      this.userService.counters = res;
      console.log(res);
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

}
