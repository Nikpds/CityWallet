import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { DebtListComponent } from './debt-list/debt-list.component';

import { DebtService } from './debt.service';
import { AuthGuard } from '../auth/auth.guard';
import { PaymentDetailsComponent } from './payment-details/payment-details.component';
import { PaymentListComponent } from './payment-list/payment-list.component';
import { DebtsToPayComponent } from './debts-to-pay/debts-to-pay.component';
import { CreditCardComponent } from './credit-card/credit-card.component';
import { PaymentOverviewComponent } from './payment-overview/payment-overview.component';

const routes: Route[] = [
  { path: 'debts', component: DebtListComponent, canActivate: [AuthGuard] },
  { path: 'debts/payment', component: PaymentDetailsComponent, canActivate: [AuthGuard] }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    HttpModule,
    FormsModule
  ],
  declarations: [
    DebtListComponent,
    PaymentDetailsComponent,
    PaymentListComponent,
    DebtsToPayComponent,
    CreditCardComponent,
    PaymentOverviewComponent
  ], providers: [
    DebtService
  ]
})
export class DebtModule { }
