import { Injectable,ViewContainerRef } from '@angular/core';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';

@Injectable()
export class NotifyService {

  constructor(
    private snotifyService: SnotifyService
  ) {  }

  success(msg: string, title?: string) {
    title = title || 'Επιτυχία';
    this.snotifyService.success(msg, title);
  }

}
