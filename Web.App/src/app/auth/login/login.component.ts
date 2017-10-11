import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../auth.service';
import { LoaderService } from '../../shared/loader.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  username: string;
  password: string;

  constructor(
    private authService: AuthService,
    private loader: LoaderService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  login() {
   // this.loader.show();
    this.authService.login(this.username, this.password).subscribe(res => {
      if (res) {
       // this.loader.hide()
        this.router.navigate(['/home']);
      } else {
        this.loader.hide()
      }
    }, error => {
     // this.loader.hide()
    });
  }

}
