import { Component, OnInit } from '@angular/core';
import { Language, LocaleService } from 'angular-l10n';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.sass']
})
export class ResetPasswordComponent implements OnInit {
  @Language() lang;

  constructor(
    public locale: LocaleService
  ) { }

  ngOnInit() {
  }

  selectLocale(language: string, country: string): void {
    this.locale.setDefaultLocale(language, country);
  }

}
