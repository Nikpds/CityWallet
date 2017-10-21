import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoaderService } from './loader.service';
import { UtilityService } from './utility.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [

  ],
  providers: [
    LoaderService,
    UtilityService
  ]

})
export class SharedModule { }
