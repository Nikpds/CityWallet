import { Component, OnInit } from '@angular/core';
import { Language, LocaleService, TranslationService } from 'angular-l10n';
import { SnotifyService } from 'ng-snotify';

import { LoaderService } from '../../shared/loader.service';
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
    private service: UserService,
    private translation: TranslationService
  ) { }

  ngOnInit() {
  }

  selectLocale(language: string, country: string): void {
    this.locale.setDefaultLocale(language, country);
  }

  reserPassword() {
    if (!this.email) { return; }
    this.loader.show();
    this.service.requestResetPassword(this.email).subscribe(res => {
      this.loader.hide();
      this.notify.success(this.translation.translate('Snotify.PswChanged'), this.translation.translate('Snotify.Success'));
    }, error => {
      this.loader.hide();
      this.notify.error(this.translation.translate('Snotify.ServerError'), this.translation.translate('Snotify.Error'));
      this.notify.error(error);
    });

  }

}
