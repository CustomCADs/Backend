import { queryOptions } from '@tanstack/react-query';
import { purchasedCartsApi as api } from '@/api';
import { Request as All } from '@/api/carts/purchased/all';
import { Request as Single } from '@/api/carts/purchased/single';

const BASE_KEY = ['purchased-carts'] as const;
export const purchasedCarts = {
	all: (params: All) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'all', params],
			queryFn: () => api.all(params),
		}),
	single: (params: Single) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params],
			queryFn: () => api.single(params),
		}),
	sortings: queryOptions({
		queryKey: [...BASE_KEY, 'sortings'],
		queryFn: api.sortings,
	}),
	paymentStatuses: queryOptions({
		queryKey: [...BASE_KEY, 'paymentStatuses'],
		queryFn: api.paymentStatuses,
	}),
	stats: queryOptions({
		queryKey: [...BASE_KEY, 'stats'],
		queryFn: api.stats,
	}),
};
