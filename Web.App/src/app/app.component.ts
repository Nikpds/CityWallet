import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { User } from './appModel';

import { SnotifyService } from 'ng-snotify';
import { AuthService } from './auth/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  private subscriptions = new Array<Subscription>();
  user: User;

  constructor(
    private snotifyService: SnotifyService,
    private auth: AuthService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => {
        this.user = user;
      }));

    this.snotifyService.success('Example body content', {
      timeout: 2000,
      showProgressBar: false,
      closeOnClick: false,
      pauseOnHover: true
    });
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }
}
