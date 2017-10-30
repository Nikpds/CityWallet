import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router'
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';

import { Bill, SettlementType, Settlement } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';
@Component({
  selector: 'app-settlement',
  templateUrl: './settlement.component.html',
  styleUrls: ['./settlement.component.sass']
})
export class SettlementComponent implements OnInit {
  private subscriptions = new Array<Subscription>();
  settlementTypes = new Array<SettlementType>();
  bills: Array<Bill>;
  installments = [];
  settlement = new Settlement();

  constructor(
    private billService: BillService,
    private router: Router,
    private loader: LoaderService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.getSettlementTypes();
    this.subscriptions.push(this.billService.billsToPay$
      .subscribe((res) => this.bills = res));

    if (this.bills.length == 0) {
      this.router.navigate(['/bills']);
    }
  }
  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  goHome() {
    this.billService.billsToPay = new Array<Bill>();
    this.router.navigate(['/']);
  }

  getSettlementTypes() {
    this.loader.show();
    this.billService.getSettlementTypes().subscribe(res => {
      this.settlementTypes = res;
      this.createMaxInstallments(this.settlementTypes[0].installments);
      this.loader.hide();
    }, error => {
      this.loader.hide();
      this.notify.error(error);
    })
  }

  createMaxInstallments(maxInstallments: number) {
    this.installments = [];
    this.settlement.installments = maxInstallments;
    let maxloop = maxInstallments / 3;
    for (let i = 0; i < maxloop; i++) {
      this.installments.push(maxInstallments);
      maxInstallments = maxInstallments - 3;
    }
    maxInstallments - 3;
  }

  submitSettlement() {
    this.loader.show();
    this.settlement.bills = this.bills;
    this.settlement.downpayment = 500;
    this.settlement.interest = 10;
    this.billService.sumbitSettletment(this.settlement).subscribe(res => {
      this.settlement = res;
      this.loader.hide();
      this.notify.success('Το αίτημα διακανονισμού καταχωρήθηκε');
    }, error => {
      this.loader.hide();
      this.notify.error("Σφάλμα καταχώρησης διακανονισμού.");
    });
  }
}