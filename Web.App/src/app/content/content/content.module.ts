import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CustomFormsModule } from 'ng2-validation';
import { SharedModule } from '../../shared/shared.module';
import { TermsofuseComponent } from '../termsofuse/termsofuse.component';
import { FaqComponent } from '../faq/faq.component';
import { ContactusComponent } from '../contactus/contactus.component';
import { ContactredirComponent } from '../contactredir/contactredir.component';

const routes: Route[] = [
  { path: 'FAQ', component: FaqComponent },
  { path: 'TermsOfUse', component: TermsofuseComponent },
  {path: 'ContactUs', component: ContactusComponent},
  {path: 'Contact', component: ContactredirComponent}
];



@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    HttpModule,
    FormsModule,
    CustomFormsModule,
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [
    TermsofuseComponent,
    FaqComponent,
    ContactusComponent,
    ContactredirComponent
  ]
})
export class ContentModule { }

