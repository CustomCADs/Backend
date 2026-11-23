import { Info, SHIPMENTS_BASE_PATH } from '../common';

export type Request = {
	id: string;
};

export type Response = {
	id: string;
	requestedAt: string;
	status: string;
	info: Info;
	fee: number;
};

export const url = (req: Request) => `${SHIPMENTS_BASE_PATH}/${req.id}`;
