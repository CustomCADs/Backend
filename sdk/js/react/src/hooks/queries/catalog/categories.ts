import { queryOptions } from '@tanstack/react-query';
import { categoriesApi as api } from '@/api';
import { Request as Single } from '@/api/catalog/categories/single';

const BASE_KEY = ['categories'] as const;
export const categories = {
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
