import { infiniteQueryOptions, queryOptions } from '@tanstack/react-query';
import { notificationsApi as api } from '@/api';
import { Request as All } from '@/api/notifications/notifications/all';

const BASE_KEY = ['notifications'] as const;
export const notifications = {
	all: (params: Omit<All, 'page'>) =>
		infiniteQueryOptions({
			queryKey: [...BASE_KEY, 'all', params] as const,
			queryFn: async ({ pageParam: page }) =>
				(await api.all({ ...params, page })).data,
			initialPageParam: 1,
			getNextPageParam: (last, all) => {
				const loaded = all.flatMap((p) => p.items).length;
				return loaded < last.count ? all.length + 1 : undefined;
			},
		}),
	sortings: queryOptions({
		queryKey: [...BASE_KEY, 'sortings'] as const,
		queryFn: api.sortings,
	}),
	statuses: queryOptions({
		queryKey: [...BASE_KEY, 'statuses'] as const,
		queryFn: api.statuses,
	}),
	stats: queryOptions({
		queryKey: [...BASE_KEY, 'stats'] as const,
		queryFn: api.stats,
	}),
};
