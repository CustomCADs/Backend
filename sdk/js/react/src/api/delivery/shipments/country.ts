import { SHIPMENTS_BASE_PATH } from '../common';

export type Request = { name: string };

export type Response = { countryId: number };

export const url = (req: Request) =>
	`${SHIPMENTS_BASE_PATH}/country/${req.name}`;
