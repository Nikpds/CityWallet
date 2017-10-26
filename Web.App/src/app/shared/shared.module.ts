import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';

import { PaginationComponent } from './pagination/pagination.component';

import { LoaderService } from './loader.service';
import { UtilityService } from './utility.service';
import { PaginationService } from './pagination.service';

@NgModule({
  imports: [
    CommonModule,
    NgxPaginationModule,
  ],
  declarations: [
    PaginationComponent
  ],
  providers: [
    LoaderService,
    UtilityService,
    PaginationService
  ],
  exports: [
    NgxPaginationModule,
    PaginationComponent
   
  ]

})
export class SharedModule { }
