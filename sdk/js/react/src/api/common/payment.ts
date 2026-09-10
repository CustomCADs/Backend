import { Address, Contact } from '../delivery/common';

export type ToAddress = { destination: 'address'; address: Address };
export type ToOffice = { destination: 'office'; officeId: number };

export type DeliveryDestination =
	| ToAddress['destination']
	| ToOffice['destination'];

export type Delivery = {
	shipmentService: string;
	contact: Contact;
} & (ToAddress | ToOffice);

export type Request = { paymentMethodId: string };
export type RequestWithDelivery = Request & Delivery;

export type Response = {
	clientSecret: string;
	message: string;
};
