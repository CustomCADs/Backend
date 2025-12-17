import { queryOptions } from '@tanstack/react-query';
import { purchasedCartsApi as api } from '@/api';
import { Request as All } from '@/api/carts/purchased/all';
import { Request as Single } from '@/api/carts/purchased/single';

const BASE_KEY = ['purchased-carts'] as const;
export const purchasedCarts = {
	all: (params: All) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'all', params] as const,
			queryFn: () => api.all(params),
		}),
	single: (params: Single) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params] as const,
			queryFn: () => api.single(params),
		}),
	sortings: queryOptions({
		queryKey: [...BASE_KEY, 'sortings'] as const,
		queryFn: api.sortings,
	}),
	paymentStatuses: queryOptions({
		queryKey: [...BASE_KEY, 'paymentStatuses'] as const,
		queryFn: api.paymentStatuses,
	}),
	stats: queryOptions({
		queryKey: [...BASE_KEY, 'stats'] as const,
		queryFn: api.stats,
	}),
};
