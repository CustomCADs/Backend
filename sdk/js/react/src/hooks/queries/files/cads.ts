import { queryOptions } from '@tanstack/react-query';
import { cadsApi as api } from '@/api';
import { Request as All } from '@/api/files/cads/all';
import { Request as Single } from '@/api/files/cads/single';

const BASE_KEY = ['cads'] as const;
export const cads = {
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
};
