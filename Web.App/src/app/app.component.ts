import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Language, LocaleService } from 'angular-l10n';

import { User, Counter } from './appModel';

import { AuthService } from './auth/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit, OnDestroy {
  @Language() lang: string;
  private subscriptions = new Array<Subscription>();
  user: User;

  constructor(
    public locale: LocaleService,
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

  selectLocale(language: string, country: string): void {
    this.locale.setDefaultLocale(language, country);
  }

}
