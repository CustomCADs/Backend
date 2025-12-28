import { queryOptions } from '@tanstack/react-query';
import { categoriesApi as api } from '@/api';
import { Request as Single } from '@/api/catalog/categories/single';

const BASE_KEY = ['categories'] as const;
export const categories = {
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
