import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { SnotifyService } from 'ng-snotify';

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
    trigger('glow', [
      state('low', style({
        boxShadow: '0px 0px 20px 4px rgba(255, 255, 255, 0.22)'
        //backgroundColor: 'red'
      })),
      state('high', style({
        boxShadow: '0px 0px 20px 4px rgba(255, 255, 255, 0.78)'
        // backgroundColor: 'yellow'
      })),
      transition('low <=> high', animate(2500))
    ])
  ]
})
export class LoginComponent implements OnInit, AfterViewInit {
  username: string;
  password: string;

  gloweffect = false;
  effect = 'low';

  constructor(
    private authService: AuthService,
    private loader: LoaderService,
    private router: Router,
    private notify: SnotifyService
  ) {
    this.notify.config.toast.timeout = 5000;
    this.notify.config.toast.pauseOnHover = true;
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    var vm = this;
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
      if (res) {
        this.loader.hide()
        this.router.navigate(['/home']);
        this.notify.error("Συνδεθήκατε", { icon: "../../../assets/check.png" })
      } else {
        this.loader.hide();
        this.notify.error("Λάθος όνομα χρήστη ή κωδικός!", { icon: "../../../assets/warning.png" })
      }
    }, error => {
      this.loader.hide();
      this.notify.error("Λάθος όνομα χρήστη ή κωδικός!", { icon: "../../../assets/warning.png" })
    });
  }

  changeState() {
    this.gloweffect = !this.gloweffect;
    this.effect = this.gloweffect ? 'low' : 'high';
  }

}
