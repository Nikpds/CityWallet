import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Language } from 'angular-l10n';
import { ActivatedRoute, Router } from '@angular/router'
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
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
  resetpwdForm: FormGroup;
  confirmPassword: string;

  constructor(
    private notify: SnotifyService,
    private loader: LoaderService,
    private service: UserService,
    private route: ActivatedRoute,
    private auth: AuthService,
    private router: Router,
    private fb: FormBuilder
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
    this.createForm();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  cancel() {
    this.isReset ? this.router.navigate(['/login']) : this.router.navigate(['/profile']);
  }

  private createForm() {
    this.resetpwdForm = this.fb.group({
      oldPassword: [this.resetPwd.oldPassword, !this.isReset ? Validators.required : null],
      password: [this.resetPwd.newPassword, Validators.compose([Validators.required, Validators.minLength(3)])],
      confirmpsw: [this.confirmPassword, Validators.required],
    });

    this.resetpwdForm.valueChanges.subscribe(value => this.onValueChanged(value));
    this.onValueChanged();
  }

  onValueChanged(value?: any) {
    if (!this.resetpwdForm) { return; }
    const form = this.resetpwdForm;

    for (const field in this.formErrors) {
      // clear previous error message (if any)
      this.formErrors[field] = '';
      const control = form.get(field);

      if (value == "submit") {
        control.markAsDirty();
      }

      if (field == 'confirmpsw') {
        let pwd = form.value.password;
        let cpwd = form.value.confirmpsw;
        if (pwd != cpwd) {
          control.setErrors({ mismatch: true });
        }
      }

      if (control && control.dirty && !control.valid) {
        const messages = this.validationMessages[field];
        for (const key in control.errors) {
          this.formErrors[field] += messages[key] + ' ';
        }
      }
    }
  }

  onSubmit() {
    const model = this.resetpwdForm.value;
    this.resetPwd.oldPassword = model.oldPassword;
    this.resetPwd.newPassword = model.password;
    this.confirmPassword = model.confirmpsw;
    if (!this.isReset) {
      this.changePassword();
    } else {
      this.resetPassword();
    }

  }

  formErrors = {
    'oldPassword': '',
    'password': '',
    'confirmpsw': ''
  };

  validationMessages = {
    'oldPassword': {
      'required': 'Existing password is required.'
    },
    'password': {
      'required': 'Password is required.',
      'minlength': 'Password must be at least three (3) characters long.'
    },
    'confirmpsw': {
      'required': 'Verify your password.',
      'mismatch': 'Password mismatch'
    }
  }

  changePassword() {
    this.loader.show();
    this.service.changePassword(this.resetPwd).subscribe((res) => {
      this.loader.hide();
      this.notify.success("Ο κωδικός σας άλλαξε");
      this.router.navigate(['/profile']);
    }, (error) => {
      this.loader.hide();
      this.notify.error(error);
    });
  }

  resetPassword() {
    this.loader.show();
    this.service.resetPassword(this.resetPwd).subscribe((res) => {
      this.loader.hide();
      this.notify.success("Ο κωδικός σας άλλαξε");
      this.router.navigate(['/login']);
    }, (error) => {
      this.loader.hide();
      this.notify.error(error);
    });
  }
}
