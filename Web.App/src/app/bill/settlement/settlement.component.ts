import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router'
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';
import { Language } from 'angular-l10n';

import { Bill, SettlementType, Settlement } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';
@Component({
  selector: 'app-settlement',
  templateUrl: './settlement.component.html',
  styleUrls: ['./settlement.component.sass']
})
export class SettlementComponent implements OnInit {
  @Language() lang;
  private subscriptions = new Array<Subscription>();
  settlementTypes = new Array<SettlementType>();
  bills: Array<Bill>;
  sType: number
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
      // this.router.navigate(['/bills']);
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
      this.sType = 0;
      this.createMaxInstallments();
      this.loader.hide();
    }, error => {
      this.loader.hide();
      this.notify.error(error);
    })
  }

  createMaxInstallments() {
    var maxInstallments = this.settlementTypes[this.sType].installments;
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
    this.settlement.settlementType.id = this.settlementTypes[this.sType].id;
    this.billService.sumbitSettletment(this.settlement).subscribe(res => {
      this.settlement = res;
      this.loader.hide();
      this.notify.success('Το αίτημα διακανονισμού καταχωρήθηκε');
    }, error => {
      this.loader.hide();
      this.notify.error("Σφάλμα καταχώρησης διακανονισμού.");
    });
  }

  calculateSettlement() {
    this.settlement.settlementType = new SettlementType();
    this.settlement.downpayment = this.downPayment(this.sType);
    this.settlement.subTotal = +this.getSubTotal().toFixed(2);
    this.settlement.monthlyFee = +(this.settlement.subTotal / this.settlement.installments).toFixed(2);
    this.settlement.settlementType.interest = this.settlementTypes[this.sType].interest;
  }

  removebillFromPay(i: number) {
    this.bills.splice(i, 1);
    if (this.bills.length == 0) {
      this.router.navigate(['/bills']);
    }
    this.calculateSettlement();
  }

  downPayment(index) {
    return +(this.getTotal(this.bills) * (this.settlementTypes[index].downpayment / 100)).toFixed(2);
  }

  getTotal(bills: Bill[]) {
    return (bills.reduce(function (a, b) { return a + b.amount }, 0));
  }

  clearSettlement() {
    this.settlement.settlementType = null;
  }

  getSubTotal() {
    return ((this.getTotal(this.bills) - this.settlement.downpayment) * (this.settlementTypes[this.sType].interest / 100)) + (this.getTotal(this.bills) - this.settlement.downpayment);
  }
}