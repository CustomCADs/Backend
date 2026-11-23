import { IDENTITY_BASE_PATH, ViewedProduct, Fingerprint } from '../common';

export type Response = {
	id: string;
	role: string;
	username: string;
	firstName: string | null;
	lastName: string | null;
	email: string;
	trackViewedProducts: boolean;
	createdAt: string;
	viewedProducts: ViewedProduct[];
	fingerprints: Fingerprint[];
};

export const url = () => `${IDENTITY_BASE_PATH}/my-account`;
