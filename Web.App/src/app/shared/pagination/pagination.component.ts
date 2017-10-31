import { Component, OnInit, Input } from '@angular/core';
import { Language } from 'angular-l10n';

import { Config } from '../../appModel';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.sass']
})
export class PaginationComponent implements OnInit {
  @Input() config = new Config();
  @Input() data = new Array();
  @Language() lang;
  
  constructor(
  ) { }

  ngOnInit() {

  }

  nextPage() {
    if (this.config.currentPage == this.config.totalPages) { return; }
    this.config.currentPage += 1
  }

  previousPage() {
    if (this.config.currentPage == 1) { return; }
    this.config.currentPage -= 1;
  }

  firstPage() {
    if (this.config.currentPage == 1) { return; }
    this.config.currentPage = 1
  }

  lastPage() {
    if (this.config.currentPage == this.config.totalPages) { return; }
    this.config.currentPage = this.config.totalPages;
  }

  setPage(page: number) {
    if (page == this.config.currentPage) {
      return;
    }
    this.config.currentPage = page;
  }

}
