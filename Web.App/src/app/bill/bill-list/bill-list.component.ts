import { Component, OnInit } from '@angular/core';
import { SnotifyService } from 'ng-snotify';

import { Bill, User } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.sass']
})
export class BillListComponent implements OnInit {
  billType = 0;

  constructor(
    private billService: BillService,
    private loader: LoaderService,
    private notify: SnotifyService
  ) { }

  ngOnInit() {
  }

  changeView(t: number) {
    this.billType = t;
  }


}
