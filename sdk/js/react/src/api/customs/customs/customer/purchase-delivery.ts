import * as headers from '@/api/common/headers';
import * as payment from '@/api/common/payment';
import { CUSTOMS_CUSTOMER_BASE_PATH } from '@/api/customs/common';

type CustomData = { id: string; count: number; customizationId: string };
type PurchaseMetadata = payment.RequestWithDelivery & headers.IdempotencyKey;
export type Request = CustomData & PurchaseMetadata;
export type Response = payment.Response;

export const url = (destination: payment.DeliveryDestination) =>
	`${CUSTOMS_CUSTOMER_BASE_PATH}/purchase-delivery/${destination}`;
