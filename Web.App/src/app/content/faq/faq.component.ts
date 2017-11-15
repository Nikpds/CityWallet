import { Component, OnInit } from '@angular/core';
import { Faq } from '../../appModel';
import {FAQS} from './faq';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.component.html',
  styleUrls: ['./faq.component.sass']
})
export class FaqComponent implements OnInit {
  faqs = FAQS;

  constructor() { }

  ngOnInit() {
  }

}
