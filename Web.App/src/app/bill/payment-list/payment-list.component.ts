import { Component, OnInit } from '@angular/core';
import { SnotifyService } from 'ng-snotify';

import { Payment } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.sass']
})
export class PaymentListComponent implements OnInit {
  payments = new Array<Payment>();

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.getPayments();
  }

  getPayments() {
    this.loader.show();
    this.billService.getPayments('0f050542-ae0e-47d9-a198-8c3501eac38a').subscribe(res => {
      this.payments = res;
      console.log(res);
      this.loader.hide();
    }, error => {
      this.loader.hide();
      this.notify.error('Αδυναμία συλλογής πληρωμών!')
    });
  }

}
