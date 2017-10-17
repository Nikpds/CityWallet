import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { User } from '../../appModel';
import { UserService } from '../user.service';
import { AuthService } from '../../auth/auth.service';
import { LoaderService } from '../../shared/loader.service';
@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.sass']
})
export class DetailsComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  user = new User();

  constructor(
    private userService: UserService,
    private auth: AuthService,
    private loader: LoaderService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.auth.user$
      .subscribe((user) => this.user = user));
    this.userService.getUser(this.user.id).subscribe(res => {
      this.user = res;
    }, error => {

    });
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

}
