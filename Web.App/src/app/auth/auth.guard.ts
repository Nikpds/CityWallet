import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router } from '@angular/router';

import { AuthService } from './auth.service';


@Injectable()
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  canActivate() {
    if (!this.authService.authenticated) {
      this.authService.user = null;
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
