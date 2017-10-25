import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs/Rx';

import { environment } from '../../environments/environment';
import { User, Counter } from '../appModel';

import { AuthService } from '../auth/auth.service';

@Injectable()
export class UserService {
  private userUrl = environment.api + 'user';

  private countersSubject$ = new BehaviorSubject<Counter>(new Counter());
  counters$ = this.countersSubject$.asObservable();

  get counters(): Counter {
    return this.countersSubject$.getValue();
  }

  set counters(value: Counter) {
    this.countersSubject$.next(value);
  }

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

}
