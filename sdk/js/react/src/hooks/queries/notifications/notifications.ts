import { infiniteQueryOptions, queryOptions } from '@tanstack/react-query';
import { notifications as api } from '@/api';
import { Request as All } from '@/api/notifications/notifications/all';

const BASE_KEY = ['notifications'] as const;
export const notifications = {
	all: (params: All) =>
		infiniteQueryOptions({
			queryKey: [...BASE_KEY, 'all', params],
			queryFn: async () => (await api.all(params)).data,
			getNextPageParam: (last, all) => {
				const loaded = all.flatMap((p) => p.items).length;
				return loaded < last.count ? all.length + 1 : undefined;
			},
			initialData: { pages: [], pageParams: [] },
			initialPageParam: 1,
			enabled: true,
		}),
	sortings: queryOptions({
		queryKey: [...BASE_KEY, 'sortings'],
		queryFn: async () => (await api.sortings()).data,
	}),
	statuses: queryOptions({
		queryKey: [...BASE_KEY, 'statuses'],
		queryFn: async () => (await api.statuses()).data,
	}),
	stats: queryOptions({
		queryKey: [...BASE_KEY, 'stats'],
		queryFn: async () => (await api.stats()).data,
	}),
};
