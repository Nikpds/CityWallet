import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { User } from './appModel';

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

    private auth: AuthService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  logout() {
    this.auth.logOut();
  }
}
