import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router'
import { Subscription } from 'rxjs/Subscription';
import { SnotifyService } from 'ng-snotify';
import { Language } from 'angular-l10n';

import { Bill, SettlementType, Settlement, NavigationC } from '../../appModel';

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
  sType: number;
  navbar = new Array<NavigationC>();
  installments = [];
  settlement = new Settlement();

  constructor(
    private billService: BillService,
    private router: Router,
    private loader: LoaderService,
    private notify: SnotifyService
  ) {

    this.navbar = [{
      icon: "fa-home", title: "App.Home", navigateTo: () => this.goHome(), isActive: false
    }, {
      icon: "fa-file-text", title: "Home.Bills", navigateTo: () => this.goBills(), isActive: false
    }, {
      icon: "fa-handshake-o", title: "Common.SettlementBtn", navigateTo: () => null, isActive: true
    }];
  }

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

  goBills() {
    this.router.navigate(['/bills']);
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
    if (this.settlement.settlementType) {
      this.calculateSettlement()
    }
  }

  submitSettlement() {
    this.loader.show();
    this.settlement.bills = this.bills;
    this.settlement.settlementType.id = this.settlementTypes[this.sType].id;
    this.billService.sumbitSettletment(this.settlement).subscribe(res => {
      this.loader.hide();
      this.notify.success('Το αίτημα διακανονισμού καταχωρήθηκε');
      this.router.navigate(['/settlement', res.id, 'details']);
    }, error => {
      this.loader.hide();
      this.notify.error("Σφάλμα καταχώρησης διακανονισμού.");
    });
  }

  reCalculate() {
    if (this.settlement.settlementType) {
      this.calculateSettlement()
    }
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
    if (!this.settlement.settlementType) { return; }
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
    this.settlement.title = null;
  }

  getSubTotal() {
    return ((this.getTotal(this.bills) - this.settlement.downpayment) * (this.settlementTypes[this.sType].interest / 100)) + (this.getTotal(this.bills) - this.settlement.downpayment);
  }
}