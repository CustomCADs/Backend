import { mutationOptions } from '@tanstack/react-query';
import { shipments as api } from '@/api';
import { Request as Cancel } from '@/api/delivery/shipments/cancel';

const BASE_KEY = ['shipments'] as const;
export const shipments = {
	cancel: mutationOptions({
		mutationKey: [...BASE_KEY, 'cancel'],
		mutationFn: async (params: Cancel) => (await api.cancel(params)).data,
	}),
};
