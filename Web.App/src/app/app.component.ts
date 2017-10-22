import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';

import { User } from './appModel';

import { AuthService } from './auth/auth.service';
import { UserService } from './user/user.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  user: User;

  constructor(
    private userService: UserService,
    private auth: AuthService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
    //this.getUser();
  }
  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  logout() {
    this.auth.logOut();
  }

  getUser() {
    if (this.user && this.user.id) {
      this.userService.getUser(this.user.id).subscribe(res => {
        this.auth.user = res;
      }, error => {
        this.notify.error(error, 'Σφάλμα!');
      });
    }
  }
}
