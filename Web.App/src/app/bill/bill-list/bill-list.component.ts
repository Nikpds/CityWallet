import { Component, OnInit } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { Router } from '@angular/router'
import { Language } from 'angular-l10n';

import { Bill, User, NavigationC } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.sass']
})
export class BillListComponent implements OnInit {
  billType = 0;
  @Language() lang;
  navbar = new Array<NavigationC>();

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private router: Router,
    private notify: SnotifyService
  ) {
    this.navbar = [{
      icon: "fa-home", title: "App.Home", navigateTo: () => this.goHome(), isActive: false
    }, {
      icon: "fa-file-text-o", title: "Home.Bills", navigateTo: () => null, isActive: true
    }];
  }

  ngOnInit() {
  }

  
  goHome() {
    this.router.navigate(['/']);
  }

  changeView(t: number) {
    this.billType = t;
  }


}
