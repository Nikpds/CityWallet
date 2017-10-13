import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoaderService } from './loader.service';
import { NotificationService } from './notification.service';
@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [

  ],
  providers: [
    LoaderService,
    NotificationService
  ]

})
export class SharedModule { }
