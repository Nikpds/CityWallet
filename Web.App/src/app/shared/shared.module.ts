import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';
import { TranslationModule, LocalizationModule, LocaleValidationModule, LocaleService, TranslationService } from 'angular-l10n';


import { PaginationComponent } from './pagination/pagination.component';

import { LoaderService } from './loader.service';
import { UtilityService } from './utility.service';
import { PaginationService } from './pagination.service';
import { NavigationComponent } from './navigation/navigation.component';
import { FilterPipe } from './filter.pipe';
 
@NgModule({
  imports: [
    CommonModule,
    NgxPaginationModule,
    TranslationModule.forRoot(),
    LocalizationModule.forRoot(),
    LocaleValidationModule.forRoot()
  ],
  declarations: [
    PaginationComponent,
    NavigationComponent,
    FilterPipe
  ],
  providers: [
    LoaderService,
    UtilityService,
    PaginationService
  ],
  exports: [
    NgxPaginationModule,
    PaginationComponent,
    TranslationModule,
    LocalizationModule,
    LocaleValidationModule,
    NavigationComponent,
    FilterPipe
  ]

})
export class SharedModule {
  constructor(public locale: LocaleService, public translation: TranslationService) {
    this.locale.addConfiguration()
      .addLanguages(['en', 'el'])
      .setCookieExpiration(30)
      .defineLanguage('el');

    this.translation.addConfiguration()
      .addProvider('./locale-');

    this.translation.init();
  }
 }
