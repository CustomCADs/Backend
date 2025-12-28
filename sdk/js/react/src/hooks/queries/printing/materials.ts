import { queryOptions } from '@tanstack/react-query';
import { materialsApi as api } from '@/api';
import { Request as Single } from '@/api/printing/materials/single';

const BASE_KEY = ['materials'] as const;
export const materials = {
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
