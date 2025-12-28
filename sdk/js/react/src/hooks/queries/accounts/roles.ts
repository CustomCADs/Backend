import { queryOptions } from '@tanstack/react-query';
import { rolesApi as api } from '@/api';
import { Request as Single } from '@/api/accounts/roles/single';

const BASE_KEY = ['roles'] as const;
export const roles = {
	all: queryOptions({
		queryKey: [...BASE_KEY, 'all'] as const,
		queryFn: api.all,
	}),
	single: (params: Single) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params] as const,
			queryFn: () => api.single(params),
		}),
};
