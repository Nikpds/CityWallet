export class User {
    name: string;
    lastname: string;
    vat: string;
    email: string;
    mobile: string;
    address: Address;
    bills: Array<Bill>;
    counters: Counter;

    constructor() {
        this.bills = new Array<Bill>();
        this.counters = new Counter();
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
    settlementType: SettlementType
    monthlyFee: number;
    subTotal: number;
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
    billId: string;
    bill_Id: string;
}

export class CreditCard {
    cardNumber: string;
    owner: string;
    cvv: string;
    expires: Date;
}

export class Counter {
    bills = 0;
    payments = 0;
    settlements = 0;
}

export class Config {
    currentPage: number;
    pageSize: number;
    orderBy: string;
    descending: boolean;
    totalCount: number;
    totalPages: number;
    data = new Array();
    searchFilter: string;
    pages: number[];
}

export class NavigationC {
    icon: string;
    title: string;
    navigateTo: Function;
    isActive: boolean;
}

export class SettlementType {
    id: string;
    downpayment: number;
    installments: number;
    interest: number;
}

export class PasswordReset {
    username: string;
    oldPassword: string;
    newPassword: string;
    verificationToken: string;

}

