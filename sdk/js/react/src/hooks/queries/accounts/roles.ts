import { queryOptions } from '@tanstack/react-query';
import { rolesApi as api } from '@/api';
import { Request as Single } from '@/api/accounts/roles/single';

const BASE_KEY = ['roles'] as const;
export const roles = {
	all: queryOptions({
		queryKey: [...BASE_KEY, 'all'],
		queryFn: async () => (await api.all()).data,
	}),
	single: (params: Single) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params],
			queryFn: async () => (await api.single(params)).data,
		}),
};
