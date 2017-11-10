import { Component, OnInit, Input } from '@angular/core';
import { Language } from 'angular-l10n';

import { NavigationC } from '../../appModel';
@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent implements OnInit {
  @Input() navigation = new Array<NavigationC>();
  @Language() lang;
  constructor() { }

  ngOnInit() {
  }

}
