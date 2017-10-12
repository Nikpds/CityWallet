import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';

import { environment } from '../../environments/environment';

import { Debt } from '../appModel';

import { AuthService } from '../auth/auth.service';

@Injectable()
export class DebtService {
  private debtUrl = environment.api + 'debt';
  userId = "";
  constructor(
    private auth: AuthService
  ) { }

  getDebts(): Observable<Array<Debt>> {
    return this.auth.get(this.debtUrl + "/debts/" + this.userId)
      .map((res: Response) => res.json())
      .catch((error: string) => Observable.throw(error || 'Server error'))
  }
}
