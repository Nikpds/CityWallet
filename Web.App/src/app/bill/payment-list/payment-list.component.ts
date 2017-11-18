import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { SnotifyService } from 'ng-snotify';
import { Language } from 'angular-l10n';

import { Payment, NavigationC, Config } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';
import { PaginationService } from '../../shared/pagination.service';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.sass']
})
export class PaymentListComponent implements OnInit {
  @Language() lang;
  searchText: string;
  filterText: string;
  config = new Config();
  payments = new Array<Payment>();
  navbar = new Array<NavigationC>();

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private router: Router,
    private notify: SnotifyService,
    private srv: PaginationService
  ) { }

  ngOnInit() {
    this.navbar = [{
      icon: "fa-home", title: "App.Home", navigateTo: () => this.goHome(), isActive: false
    }, {
      icon: "fa-money", title: "Home.Payments", navigateTo: () => null, isActive: true
    }];
    this.getPayments();
  }

  initConfig(data: any) {
    this.config.currentPage = 1;
    this.config.pageSize = 10;
    this.config.data = data;
    this.config.totalPages = this.getTotalPages(data.length, this.config.pageSize);
    this.config.pages = this.srv.getPages(this.config.totalPages, this.config.currentPage);
  }

  getTotalPages(length: number, size: number) {
    let pageSize = Math.floor(length / size);
    if (pageSize != length / size) {
      return pageSize + 1
    }
    return pageSize
  }

  getPayments() {
    this.loader.show();
    this.billService.getPayments().subscribe(res => {
      this.payments = res;
      this.initConfig(res);;
      this.loader.hide();
    }, error => {
      this.loader.hide();
      this.notify.error('Αδυναμία συλλογής πληρωμών!')
    });
  }

  goHome() {
    this.router.navigate(['/']);
  }

  print() {
    window.print();
  }

  Search(t: string) {
    this.searchText = t.trim();
    this.filterText = t.trim();
  }

  TotalDebt() {
    return (this.payments.reduce(function (a, b) { return a + b.bill.amount }, 0)).toFixed(2);
  }

  paymentPackage(i: number) {
    if (i == 0) { return true; }
    let o = new Date(this.payments[i - 1].paidDate);
    let d = new Date(this.payments[i].paidDate);
    if (o.getHours() == d.getHours() && o.getMinutes() == d.getMinutes()) { return false; }
    return true;
  }
  getPackageLength(p: Payment) {
    let count = 0
    let o = new Date(p.paidDate);
    for (let i = 0; i < this.payments.length; i++) {
      let d = new Date(this.payments[i].paidDate);
      if (o.getHours() == d.getHours() && o.getMinutes() == d.getMinutes()) {
        count++;
      }
    }
    return count;
  }

  getTotalOfPackage(p: Payment) {
    let sum = 0
    let o = new Date(p.paidDate);
    for (let i = 0; i < this.payments.length; i++) {
      let d = new Date(this.payments[i].paidDate);
      if (o.getHours() == d.getHours() && o.getMinutes() == d.getMinutes()) {
        sum= sum + this.payments[i].bill.amount;
      }
    }
    return sum;
  }
}
