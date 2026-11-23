import { SHIPMENTS_BASE_PATH } from '../common';

export type Request = {
	shipmentId: string;
};

export type Response = {
	id: string;
	service: string;
};

export const url = (req: Request) =>
	`${SHIPMENTS_BASE_PATH}/${req.shipmentId}/reference`;
