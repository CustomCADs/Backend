import { queryOptions } from '@tanstack/react-query';
import { accountsApi as api } from '@/api';
import { Request as All } from '@/api/accounts/accounts/all';
import { Request as Single } from '@/api/accounts/accounts/single';

const BASE_KEY = ['accounts'] as const;
export const accounts = {
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
};
