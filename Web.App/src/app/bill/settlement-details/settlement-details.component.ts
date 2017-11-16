import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Route, ActivatedRoute, Params } from '@angular/router'
import { Language } from 'angular-l10n';

import { Settlement } from '../../appModel';
import { BillService } from '../bill.service';
@Component({
  selector: 'app-settlement-details',
  templateUrl: './settlement-details.component.html',
  styleUrls: ['./settlement-details.component.sass']
})
export class SettlementDetailsComponent implements OnInit {
  @Language() lang;
  private subscriptions = new Array<Subscription>();
  settlement :Settlement;
  constructor(
    private activeRoute: ActivatedRoute,
    private billService: BillService
  ) { }

  ngOnInit() {
    this.activeRoute.params.subscribe((param: Params) => {
      let id = param['id'];
      if (id) {
        this.billService.getSettlement(id).subscribe(res => {
          console.log(res);
          this.settlement = res;
        }, error => {

        });
      }
    });
  }

  print() {
    window.print();
  }
}
