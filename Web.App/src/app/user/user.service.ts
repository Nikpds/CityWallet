import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs/Rx';

import { environment } from '../../environments/environment';
import { User, Counter, PasswordReset } from '../appModel';

import { AuthService } from '../auth/auth.service';

@Injectable()
export class UserService {
  private userUrl = environment.api + 'user';

  constructor(
    private auth: AuthService
  ) { }

  getUser(): Observable<User> {
    return this.auth.get(this.userUrl + '/user')
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

  getCounters(): Observable<Counter> {
    return this.auth.get(this.userUrl + '/counters')
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

  requestResetPassword(email: string): Observable<boolean> {
    return this.auth.get(this.userUrl + '/requestpasswordreset/' + email)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

  changePassword(pwd: PasswordReset): Observable<boolean> {
    return this.auth.put(this.userUrl + '/change/password', pwd)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

  resetPassword(pwd: PasswordReset): Observable<boolean> {
    return this.auth.put(this.userUrl + '/reset/password', pwd)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

}
