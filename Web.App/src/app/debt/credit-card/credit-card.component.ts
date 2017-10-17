import { Component, OnInit, Input } from '@angular/core';

import { CreditCard } from '../../appModel';
@Component({
  selector: 'app-credit-card',
  templateUrl: './credit-card.component.html',
  styleUrls: ['./credit-card.component.sass']
})
export class CreditCardComponent implements OnInit {
  @Input() creditCard = new CreditCard();

  constructor() { }

  ngOnInit() {

  }

}
