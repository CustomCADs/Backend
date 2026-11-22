export type Geoposition = { longitude: number; latitude: number };
export type Address = {
	country: string;
	site: string;
	addressNote: string;
	coordinates: Geoposition;
};

export type Info = {
	count: number;
	weight: number;
	recipient: string;
};

export type Contact = {
	phone: string;
	email: string;
};

export const SHIPMENTS_BASE_PATH = '/shipments';
