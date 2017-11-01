import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Language } from 'angular-l10n';
import { ActivatedRoute, Router } from '@angular/router'
import { SnotifyService } from 'ng-snotify';

import { PasswordReset, User } from '../../appModel';

import { LoaderService } from "../../shared/loader.service";
import { UserService } from '../../user/user.service';
import { AuthService } from '../auth.service';
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.sass']
})
export class ChangePasswordComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  @Language() lang;
  user = new User();
  resetPwd = new PasswordReset();
  isReset = true;

  constructor(
    private notify: SnotifyService,
    private loader: LoaderService,
    private service: UserService,
    private route: ActivatedRoute,
    private auth: AuthService,
    private router: Router
  ) { }


  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
    this.isReset = this.auth.authenticated;

    if (this.auth.authenticated) {
      this.isReset = false;
      this.resetPwd.username = this.user.vat;
    }
    else {
      this.isReset = true;
      this.route.queryParams.subscribe(params => {
        let token = params['token'];
        if (token) {
          this.resetPwd.verificationToken = token;
        }
        else {
          this.notify.error("You must be logged in in order to change your password.");
          this.cancel();
        }
      });
    }

  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }


  cancel() {
    this.router.navigate(['/profile']);
  }


  changePassword() {
    this.loader.show();
    this.service.changePassword(this.resetPwd).subscribe(res => {
      console.log(res);
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

}
