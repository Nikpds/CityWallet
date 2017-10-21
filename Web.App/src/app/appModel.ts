export class User {
    id: string;
    name: string;
    lastname: string;
    vat: string;
    email: string;
    password: string;
    mobile: string;
    address: Address;
    bills: Array<Bill>;

    constructor() {
        this.bills = new Array<Bill>();
    }
}

export class Bill {
    id: string;
    description: string;
    bill_Id: string;
    amount: number;

    paymentId: string;
    payment: Payment;

    userId: string;
    user: User;

    settlementId: string;
    settlement: Settlement;

    dateDue: Date;
}

export class Address {
    id: string;
    street: string;
    county: string;
}

export class Settlement {
    id: string;
    interest: number;
    installments: number;
    downpayment: number;
    requestDate: Date;
    bills: Array<Bill>;

    constructor() {
        this.bills = new Array<Bill>();
    }
}

export class Payment {
    id: string;
    paidDate: Date;
    method: string;
    bill: Bill;
    bill_Id: string;
}

export class CreditCard {
    cardNumber: string;
    owner: string;
    cvv: string;
    expires: Date;
}