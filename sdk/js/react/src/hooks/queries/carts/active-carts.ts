import { queryOptions } from '@tanstack/react-query';
import { activeCartsApi as api } from '@/api';
import { Request as Calculate } from '@/api/carts/active/calculate-shipment';

const BASE_KEY = ['active-carts'] as const;
export const activeCarts = {
	all: queryOptions({
		queryKey: [...BASE_KEY, 'all'] as const,
		queryFn: api.all,
	}),
	count: queryOptions({
		queryKey: [...BASE_KEY, 'count'] as const,
		queryFn: api.count,
	}),
	calculateShipment: (params: Calculate) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'calculate-shipment', params] as const,
			queryFn: () => api.calculateShipment(params),
		}),
};
