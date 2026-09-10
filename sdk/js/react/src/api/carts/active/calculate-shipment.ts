import { objectToSearchParams } from '@/utils/params';
import * as calculation from '@/api/common/calculation';
import { ACTIVE_CART_BASE_PATH } from '../common';

export type Request = calculation.AddressDelivery | calculation.OfficeDelivery;
export type Response = calculation.Calculation[];

export const url = (req: Request) =>
	`${ACTIVE_CART_BASE_PATH}/calculate/${req.destination}?${objectToSearchParams(req, (key) => key !== 'destination')}`;
