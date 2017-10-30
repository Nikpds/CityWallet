import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CustomFormsModule } from 'ng2-validation'

import { BillListComponent } from './bill-list/bill-list.component';
import { PaymentComponent } from './payment/payment.component';
import { SharedModule } from '../shared/shared.module';
import { PaymentListComponent } from './payment-list/payment-list.component';
import { BillToPayComponent } from './bill-to-pay/bill-to-pay.component';
import { CreditCardComponent } from './credit-card/credit-card.component';
import { PaymentOverviewComponent } from './payment-overview/payment-overview.component';

import { BillService } from './bill.service';
import { AuthGuard } from '../auth/auth.guard';
import { BillsUnpaidComponent } from './bill-list/bills-unpaid/bills-unpaid.component';
import { BillsPaidComponent } from './bill-list/bills-paid/bills-paid.component';
import { BillsSettledComponent } from './bill-list/bills-settled/bills-settled.component';
import { SettlementComponent } from './settlement/settlement.component';

const routes: Route[] = [
  { path: 'bills', component: BillListComponent, canActivate: [AuthGuard] },
  { path: 'bills/payment', component: PaymentComponent, canActivate: [AuthGuard] },
  { path: 'payments', component: PaymentListComponent, canActivate: [AuthGuard] },
  { path: 'bills/settlement', component: SettlementComponent, canActivate: [AuthGuard] }
]

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
    BillToPayComponent,
    BillListComponent,
    PaymentComponent,
    PaymentListComponent,
    CreditCardComponent,
    PaymentOverviewComponent,
    BillsUnpaidComponent,
    BillsPaidComponent,
    BillsSettledComponent,
    SettlementComponent
  ], providers: [
    BillService
  ]
})
export class BillModule { }
