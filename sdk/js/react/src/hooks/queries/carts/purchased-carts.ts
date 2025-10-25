import { queryOptions } from '@tanstack/react-query';
import { purchasedCarts as api } from '@/api';
import { Request as All } from '@/api/carts/purchased/all';
import { Request as Single } from '@/api/carts/purchased/single';

const BASE_KEY = ['purchased-carts'] as const;
export const purchasedCarts = {
	all: (params: All) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'all', params],
			queryFn: async () => (await api.all(params)).data,
		}),
	single: (params: Single) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params],
			queryFn: async () => (await api.single(params)).data,
		}),
	sortings: queryOptions({
		queryKey: [...BASE_KEY, 'sortings'],
		queryFn: async () => (await api.sortings()).data,
	}),
	paymentStatuses: queryOptions({
		queryKey: [...BASE_KEY, 'paymentStatuses'],
		queryFn: async () => (await api.paymentStatuses()).data,
	}),
	stats: queryOptions({
		queryKey: [...BASE_KEY, 'stats'],
		queryFn: async () => (await api.stats()).data,
	}),
};
