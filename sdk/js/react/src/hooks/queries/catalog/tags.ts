import { queryOptions } from '@tanstack/react-query';
import { tagsApi as api } from '@/api';
import { Request as SingleTag } from '@/api/catalog/tags/single';

const BASE_KEY = ['tags'] as const;
export const tags = {
	all: queryOptions({
		queryKey: [...BASE_KEY, 'all'],
		queryFn: api.all,
	}),
	single: (params: SingleTag) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params],
			queryFn: () => api.single(params),
		}),
};
