import { Component, OnInit } from '@angular/core';
import { Language, LocaleService } from 'angular-l10n';
import { SnotifyService } from 'ng-snotify';

import { LoaderService } from "../../shared/loader.service";
import { UserService } from '../../user/user.service';
@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.sass']
})
export class ResetPasswordComponent implements OnInit {
  @Language() lang;
  email: string;
  
  constructor(
    public locale: LocaleService,
    private notify: SnotifyService,
    private loader: LoaderService,
    private service: UserService
  ) { }

  ngOnInit() {
  }

  selectLocale(language: string, country: string): void {
    this.locale.setDefaultLocale(language, country);
  }

  reserPassword() {
    if (!this.email) { return; }
    this.loader.show();
    this.service.resetPassword(this.email).subscribe(res => {
      this.loader.hide();
      this.notify.success("Check your email.");
    }, error => {
      this.loader.hide();
      this.notify.error(error);
    });

  }

}
