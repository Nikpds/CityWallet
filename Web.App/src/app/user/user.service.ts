import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';

import { environment } from '../../environments/environment';
import { User } from '../appModel';

import { AuthService } from '../auth/auth.service';

@Injectable()
export class UserService {
  private userUrl = environment.api + 'user';
  constructor(
    private auth: AuthService
  ) { }


  getUser(id: string): Observable<User> {
    return this.auth.get(this.userUrl + "/" + id)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }

}
