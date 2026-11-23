import { objectToSearchParams } from '@/utils/params';
import { Info, SHIPMENTS_BASE_PATH } from '../common';

export type Request = {
	sortingType?: string;
	sortingDirection?: string;
	page: number;
	limit: number;
};

export type Response = {
	id: string;
	requestedAt: string;
	status: string;
	info: Info;
};

export const url = (req: Request) =>
	`${SHIPMENTS_BASE_PATH}?${objectToSearchParams(req)}`;
