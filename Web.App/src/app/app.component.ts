import { Component, OnInit } from '@angular/core';

import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(
    private snotifyService: SnotifyService
  ) { }

  ngOnInit() {
    this.snotifyService.success('Example body content', {
      timeout: 2000,
      showProgressBar: false,
      closeOnClick: false,
      pauseOnHover: true
    });
  }
}
