export type AddressDelivery = {
	destination: 'address';
	country: string;
	site: string;
};
export type OfficeDelivery = {
	destination: 'office';
	officeId: number;
};

export type Calculation = {
	service: string;
	total: number;
	currency: string;
	pickupDate: string;
	deliveryDeadline: string;
};
