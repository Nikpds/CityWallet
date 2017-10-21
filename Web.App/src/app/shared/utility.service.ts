import { Injectable } from '@angular/core';

@Injectable()
export class UtilityService {
  years = new Array<number>();
  constructor() {
    this.getYears();
  }

  months = [
    "Ιανουάριος", "Φεβρουάριος", "Μάρτιος", "Απρίλιος", "Μάιος", "Ιούνιος",
    "Ιούλιος", "Αύγουστος", "Σεπτέμβριος", "Οκτώβριος", "Νοέμβριος", "Δεκέμβριος"
  ]

  parseEnum(_enum): Array<any> {
    let map: { id: number; name: string }[] = [];
    for (var n in _enum) {
      if (typeof _enum[n] === 'number') {
        map.push({ id: <any>_enum[n], name: n });
      }
    }
    return map;
  }

  parseEnumName(type, value): string {
    for (var n in type) {
      if (type[n] === value) {
        return n;
      }
    }
    return '';
  }

  getYears() {
    var current = new Date().getFullYear();
    for (var i = 0; i < 10; i++) {
      this.years.push(current + i);
    }
  }



}
