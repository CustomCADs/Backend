import * as headers from '@/api/common/headers';
import * as payment from '@/api/common/payment';
import { ACTIVE_CART_BASE_PATH } from '../common';

export type Request = headers.IdempotencyKey & payment.RequestWithDelivery;
export type Response = payment.Response;

export const url = (destination: payment.DeliveryDestination) =>
	`${ACTIVE_CART_BASE_PATH}/purchase-delivery/${destination}`;
