import { Component, OnInit } from '@angular/core';
import { SnotifyService } from 'ng-snotify';

import { Bill, SettlementType, Settlement } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';
@Component({
  selector: 'app-settlement-list',
  templateUrl: './settlement-list.component.html',
  styleUrls: ['./settlement-list.component.sass']
})
export class SettlementListComponent implements OnInit {
  settlements = new Array<Settlement>();

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
    this.getSettlements();
  }

  getSettlements() {
    this.loader.show();
    this.billService.getSettlements().subscribe(res => {
      this.settlements = res;
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  cancelSettlementRequest(id: string) {
    let i = this.settlements.findIndex(x => x.id == id);
    this.loader.show();
    this.billService.cancelSettlementRequest(id).subscribe(res => {
      this.settlements.splice(i, 1);
      this.notify.success('Ο Διακανονισμός που επιλέξατε ακυρώθηκε!','Ακύρωση Διακανονισμού')
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

}
