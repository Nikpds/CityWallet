import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnotifyModule, SnotifyService, ToastDefaults } from 'ng-snotify';

import { NotifyService } from './notify.service';

@NgModule({
  imports: [
    CommonModule,
    SnotifyModule.forRoot()
  ],
  declarations: [

  ],
  providers: [
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    SnotifyService,
    NotifyService
  ]
})
export class CoreModule { }
