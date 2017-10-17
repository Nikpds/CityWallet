import { Component, OnInit, Input } from '@angular/core';

import { Debt, User } from '../../appModel';
@Component({
  selector: 'app-debts-to-pay',
  templateUrl: './debts-to-pay.component.html',
  styleUrls: ['./debts-to-pay.component.sass']
})
export class DebtsToPayComponent implements OnInit {
  
  @Input() debts = new Array<Debt>();
  constructor() { }

  ngOnInit() {
  }

}
