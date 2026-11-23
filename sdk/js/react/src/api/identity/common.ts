export type ViewedProduct = {
	id: string;
	viewedAt: string;
};

export type Fingerprint = {
	id: string;
	device: string;
	location: string | null;
	deleteAllowed: boolean;
	issuedAt: string;
};

export const IDENTITY_BASE_PATH = '/identity';
