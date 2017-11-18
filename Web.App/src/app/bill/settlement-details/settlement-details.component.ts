import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Router } from '@angular/router'
import { Route, ActivatedRoute, Params } from '@angular/router'
import { Language } from 'angular-l10n';

import { Settlement, NavigationC } from '../../appModel';
import { BillService } from '../bill.service';
@Component({
  selector: 'app-settlement-details',
  templateUrl: './settlement-details.component.html',
  styleUrls: ['./settlement-details.component.sass']
})
export class SettlementDetailsComponent implements OnInit {
  @Language() lang;
  navbar = new Array<NavigationC>();
  private subscriptions = new Array<Subscription>();
  settlement: Settlement;
  constructor(
    private activeRoute: ActivatedRoute,
    private billService: BillService,
    private router: Router
  ) { }

  ngOnInit() {
    this.activeRoute.params.subscribe((param: Params) => {
      let id = param['id'];
      if (id) {
        this.billService.getSettlement(id).subscribe(res => {
          this.settlement = res;
          this.navbar = [{
            icon: "fa-home", title: "App.Home", navigateTo: () => this.goHome(), isActive: false
          }, {
            icon: "fa-handshake-o", title: "Home.Settlements", navigateTo: () => this.goSettlements(), isActive: false
          }, {
            icon: "fa-file-text-o", title: this.settlement.title, navigateTo: () => null, isActive: true
          }];
        }, error => {

        });
      }
    });
  }

  print() {
    window.print();
  }

  billsSum() {
    return (this.settlement.bills.reduce(function (a, b) { return a + b.amount }, 0)).toFixed(2);
  }

  goHome() {
    this.router.navigate(['/']);
  }

  goSettlements() {
    this.router.navigate(['/settlements']);
  }
}
