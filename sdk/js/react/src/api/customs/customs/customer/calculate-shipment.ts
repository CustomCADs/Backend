import { objectToSearchParams } from '@/utils/params';
import * as calculation from '@/api/common/calculation';
import { CUSTOMS_CUSTOMER_BASE_PATH } from '@/api/customs/common';

export type Request = {
	id: string;
	count: number;
	customizationId: string;
} & (calculation.AddressDelivery | calculation.OfficeDelivery);

export type Response = calculation.Calculation[];

export const url = (req: Request) =>
	`${CUSTOMS_CUSTOMER_BASE_PATH}/calculate/${req.destination}/${req.id}?${objectToSearchParams(
		req,
		(key) => key !== 'id' && key !== 'destination',
	)}`;
