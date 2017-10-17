export class User {
    id: string;
    name: string;
    lastname: string;
    vat: string;
    email: string;
    password: string;
    mobile: string;
    address: Address;
    debts: Array<Debt>;

    constructor() {
        this.debts = new Array<Debt>();
    }
}

export class Debt {
    id: string;
    description: string;
    billId: string;
    amount: number;
    userId: string;
    dateDue: Date;
}
export class Address {
    street: string;
    county: string;
    postalcode: string;
}
export class Settlement {
    id: string;
}

export class Payment {
    id: string;
}

export class SettlementRequest {
    id: string;
}

export class CreditCard {
    cardNumber: string;
    owner: string;
    cvv: string;
    expires: Date;
}