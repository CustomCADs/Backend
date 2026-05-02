import { IDENTITY_BASE_PATH, ViewedProduct } from '../common';

export type Response = {
	id: string;
	role: string;
	username: string;
	firstName?: string;
	lastName?: string;
	email: string;
	trackViewedProducts: boolean;
	viewedProducts: ViewedProduct[];
	createdAt: string;
};

export const url = () => `${IDENTITY_BASE_PATH}/my-account`;
