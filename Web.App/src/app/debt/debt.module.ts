import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { DebtListComponent } from './debt-list/debt-list.component';

import { DebtService } from './debt.service';
import { AuthGuard } from '../auth/auth.guard';

const routes: Route[] = [
  { path: 'debts', component: DebtListComponent, canActivate: [AuthGuard] }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    HttpModule,
    FormsModule
  ],
  declarations: [
    DebtListComponent
  ], providers: [
    DebtService
  ]
})
export class DebtModule { }
