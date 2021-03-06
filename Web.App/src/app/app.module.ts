import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { RouterModule, Route } from '@angular/router';
import { HttpModule } from '@angular/http';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { SharedModule } from './shared/shared.module';
import { AuthModule } from './auth/auth.module';
import { UserModule } from './user/user.module';
import { AuthGuard } from './auth/auth.guard';
import { BillModule } from './bill/bill.module';
import { ContentModule } from './content/content.module';

import { SnotifyModule, SnotifyService, ToastDefaults, SnotifyPosition } from 'ng-snotify';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { ResetPasswordComponent } from './auth/reset-password/reset-password.component';
import { ChangePasswordComponent } from './auth/change-password/change-password.component';

const routes: Route[] = [
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: "login", component: LoginComponent },
  { path: "forgotpassword", component: ResetPasswordComponent },
  { path: "changepwd", component: ChangePasswordComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    ResetPasswordComponent,
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BillModule,
    ContentModule,
    FormsModule,
    UserModule,
    ReactiveFormsModule,
    SharedModule,
    HttpModule,
    AuthModule,
    RouterModule.forRoot(routes),
    SnotifyModule
  ],
  providers: [
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    SnotifyService

  ],
  bootstrap: [AppComponent]
})
export class AppModule {

  positions = SnotifyPosition;
  
  constructor(
    private notify: SnotifyService,
  ) {
    this.notify.config.toast.timeout = 5000;
    this.notify.config.toast.pauseOnHover = true;
    this.notify.config.toast.position = this.positions.centerTop;
    this.notify.config.toast.bodyMaxLength = 300;
    this.notify.config.toast.titleMaxLength = 300;
  }
}
