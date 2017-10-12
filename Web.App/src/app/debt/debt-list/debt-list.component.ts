import { Component, OnInit } from '@angular/core';

import { Debt } from '../../appModel';

import { DebtService } from '../debt.service';
import { LoaderService } from '../../shared/loader.service';
@Component({
  selector: 'app-debt-list',
  templateUrl: './debt-list.component.html',
  styleUrls: ['./debt-list.component.sass']
})
export class DebtListComponent implements OnInit {
  debts = new Array<Debt>();
  constructor(
    private debtService: DebtService,
    private loader: LoaderService
  ) { }

  ngOnInit() {
    this.getDebts();
  }

  getDebts() {
    // this.loader.show();
    this.debtService.getDebts().subscribe((res) => {
      this.debts = res;
      console.log(res);
      this.loader.hide();
    }, error => {

      //  this.loader.hide();
    });
  }
}
