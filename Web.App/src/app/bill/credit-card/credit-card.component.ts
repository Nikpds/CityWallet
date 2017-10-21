import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { CustomValidators } from 'ng2-validation';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CreditCard } from '../../appModel';

import { UtilityService } from '../../shared/utility.service';
import { BillService } from '../bill.service';
@Component({
  selector: 'app-credit-card',
  templateUrl: './credit-card.component.html',
  styleUrls: ['./credit-card.component.sass']
})
export class CreditCardComponent implements OnInit, OnDestroy {
  private subscriptions = new Array<Subscription>();
  
  creditCard = new CreditCard();
  creditCardForm: FormGroup;
  months = this.utility.months;
  years = this.utility.years;

  constructor(
    private fb: FormBuilder,
    private utility: UtilityService,
    private billService: BillService
  ) { }

  ngOnInit() {
    this.subscriptions.push(this.billService.creditCard$
      .subscribe((res) => this.creditCard = res));

    this.creditCard.cardNumber = "4532159880756245";
    this.creditCard.cvv = "422";
    this.creditCard.expires = new Date("2020-01-01");
    this.creditCard.owner = "Nikos Perperidis";

    this.createForm();

  }

  validateCard(value: string) {

  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  private createForm() {
    this.creditCardForm = this.fb.group({
      creditCardNumber: [this.creditCard.cardNumber, CustomValidators.creditCard],
      owner: [this.creditCard.owner, Validators.required],
      cvv: [this.creditCard.cvv, Validators.compose([Validators.required, CustomValidators.number])],
    });
    this.creditCardForm.valueChanges.subscribe(value => this.onValueChanged(value));
    this.onValueChanged();
  }

  onValueChanged(value?: any) {
    if (!this.creditCardForm) { return; }
    const form = this.creditCardForm;

    for (const field in this.formErrors) {
      this.formErrors[field] = '';
      const control = form.get(field);

      if (value == "submit") {
        control.markAsDirty();
      }

      if (control && control.dirty && !control.valid) {
        const messages = this.validationMessages[field];
        for (const key in control.errors) {
          this.formErrors[field] += messages[key] + ' ';
        }
      }
    }
  }

  formErrors = {
    'creditCardNumber': '',
    'owner': '',
    'cvv': ''
  };

  validationMessages = {
    'creditCardNumber': {
      'creditCard': 'Ο αριθμός της κάρτας δεν είναι έγκυρος.'
    },
    'owner': {
      'required': 'Το Όνομα κατόχου έιναι υποχρεωτικό.'
    },
    'cvv': {
      'required': 'To CVV εγγραφής έιναι υποχρεωτικό.',
      'number': 'To CVV δεν  έιναι έγκυρο.'
    }
  }

}
