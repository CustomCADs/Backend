import { queryOptions } from '@tanstack/react-query';
import { accounts as api } from '@/api';
import { Request as All } from '@/api/accounts/accounts/all';
import { Request as Single } from '@/api/accounts/accounts/single';

const BASE_KEY = ['accounts'] as const;
export const accounts = {
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
};
