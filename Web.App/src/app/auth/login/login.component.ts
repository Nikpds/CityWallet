import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { SnotifyService } from 'ng-snotify';
import { Language, LocaleService, TranslationService } from 'angular-l10n';

import { AuthService } from '../auth.service';
import { LoaderService } from '../../shared/loader.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
  animations: [
    // trigger('fade', [
    //   transition('void => *',[
    //     style({opacity: 0}),
    //     animate(2000)
    //   ])
    // ]),
    // trigger('glow', [
    //   state('low', style({
    //     boxShadow: '0px 0px 20px 4px rgba(255, 255, 255, 0.22)'
    //   })),
    //   state('high', style({
    //     boxShadow: '0px 0px 20px 4px rgba(255, 255, 255, 0.78)'
    //   })),
    //   transition('low <=> high', animate(2500))
    // ])
  ]
})
export class LoginComponent implements OnInit, AfterViewInit {
  username: string;
  password: string;
  @Language() lang;

  gloweffect = false;
  effect = 'low';

  constructor(
    private authService: AuthService,
    private loader: LoaderService,
    private router: Router,
    private notify: SnotifyService,
    public locale: LocaleService,
    private translation: TranslationService
  ) {}

  ngOnInit() {
    if (this.authService.authenticated) {
      this.router.navigate(['/home']);
    }
  }

  ngAfterViewInit() {
    const vm = this;
    setInterval(function () {
      vm.changeState();
    }, 2500);
  }

  change() {
    this.gloweffect = !this.gloweffect;
    this.effect = this.gloweffect ? 'low' : 'high';
  }

  login() {
    this.gloweffect = !this.gloweffect;
    this.loader.show();
    this.authService.login(this.username, this.password).subscribe(res => {
      if (res == "firstLogin") {
        this.loader.hide();
        this.notify.info(this.translation.translate('Snotify.FirstLogIn'), this.translation.translate('Snotify.Info'),
          { timeout: 15000, buttons: [{ text: 'ΟΚ', action: null, bold: true }] });
      }
      else if (res) {
        this.loader.hide();
        this.router.navigate(['/home']);
        this.notify.success(this.translation.translate('Snotify.LoggedIn'), this.translation.translate('Snotify.Success'));
      } else {
        this.loader.hide();
        this.notify.error(this.translation.translate('Snotify.WrongCrend'), this.translation.translate('Snotify.Error'));
      }
    }, error => {
      this.loader.hide();
      this.notify.error(this.translation.translate('Snotify.WrongCrend'), this.translation.translate('Snotify.Error'));
    });
  }

  changeState() {
    this.gloweffect = !this.gloweffect;
    this.effect = this.gloweffect ? 'low' : 'high';
  }

  selectLocale(language: string, country: string): void {
    this.locale.setDefaultLocale(language, country);
  }


}
