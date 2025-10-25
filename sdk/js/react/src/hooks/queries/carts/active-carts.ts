import { queryOptions } from '@tanstack/react-query';
import { activeCarts as api } from '@/api';
import { Request as Calculate } from '@/api/carts/active/calculate-shipment';

const BASE_KEY = ['active-carts'] as const;
export const activeCarts = {
	all: queryOptions({
		queryKey: [...BASE_KEY, 'all'],
		queryFn: async () => (await api.all()).data,
	}),
	count: queryOptions({
		queryKey: [...BASE_KEY, 'count'],
		queryFn: async () => (await api.count()).data,
	}),
	calculateShipment: (params: Calculate) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'calculate-shipment', params],
			queryFn: async () => (await api.calculateShipment(params)).data,
		}),
};
