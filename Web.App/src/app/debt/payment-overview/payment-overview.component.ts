import { Component, OnInit, Input } from '@angular/core';

import { CreditCard } from '../../appModel';
@Component({
  selector: 'app-payment-overview',
  templateUrl: './payment-overview.component.html',
  styleUrls: ['./payment-overview.component.sass']
})
export class PaymentOverviewComponent implements OnInit {
  @Input() creditCard = new CreditCard();

  constructor() { }

  ngOnInit() {

  }


}
