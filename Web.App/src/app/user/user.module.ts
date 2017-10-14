import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { HttpModule } from '@angular/http';

import { DetailsComponent } from './details/details.component';

import { AuthGuard } from '../auth/auth.guard';
import { UserService} from './user.service';
const routes: Route[] = [
  { path: 'profile', component: DetailsComponent, canActivate: [AuthGuard] }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    HttpModule,
  ],
  declarations: [
    DetailsComponent
  ], providers: [
    UserService
  ]
})
export class UserModule { }
