import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterPipe'
})
export class FilterPipe implements PipeTransform {

  transform(items: any[], field: string, searchText: string, subField?: string): any[] {
    if (!items) return [];
    if (!searchText) return items;
    searchText = searchText.toLowerCase();
    return items.filter(it => {
      return subField ? it[field][subField].toLowerCase().includes(searchText) : it[field].toLowerCase().includes(searchText);
    });
  }
}
