import { Component, OnInit } from '@angular/core';
import { SnotifyService } from 'ng-snotify';
import { Router } from '@angular/router'
import { Language } from 'angular-l10n';

import { Bill, SettlementType, Settlement, Config, NavigationC } from '../../appModel';

import { BillService } from '../bill.service';
import { LoaderService } from '../../shared/loader.service';
import { PaginationService } from '../../shared/pagination.service';
@Component({
  selector: 'app-settlement-list',
  templateUrl: './settlement-list.component.html',
  styleUrls: ['./settlement-list.component.sass']
})
export class SettlementListComponent implements OnInit {
  settlements = new Array<Settlement>();
  @Language() lang;
  searchText: string;
  filterText: string;
  config = new Config();
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
      icon: "fa-handshake-o", title: "Home.Settlements", navigateTo: () => null, isActive: true
    }];
    this.getSettlements();
  }

  getSettlements() {
    this.loader.show();
    this.billService.getSettlements().subscribe(res => {
      this.settlements = res;
      this.initConfig(res);
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
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

  cancelSettlementRequest(id: string) {
    let i = this.settlements.findIndex(x => x.id == id);
    this.loader.show();
    this.billService.cancelSettlementRequest(id).subscribe(res => {
      this.settlements.splice(i, 1);
      this.notify.success('Ο Διακανονισμός που επιλέξατε ακυρώθηκε!', 'Ακύρωση Διακανονισμού')
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
  }

  goToDetails(i: number) {
    this.router.navigate(['settlement', this.settlements[i].id, 'details'])
  }

}
