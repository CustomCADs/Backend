import { queryOptions } from '@tanstack/react-query';
import { materialsApi as api } from '@/api';
import { Request as Single } from '@/api/printing/materials/single';

const BASE_KEY = ['materials'] as const;
export const materials = {
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
