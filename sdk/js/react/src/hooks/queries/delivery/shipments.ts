import { queryOptions } from '@tanstack/react-query';
import { shipments as api } from '@/api';
import { Request as All } from '@/api/delivery/shipments/all';
import { Request as Waybill } from '@/api/delivery/shipments/waybill';
import { Request as Track } from '@/api/delivery/shipments/track';

const BASE_KEY = ['shipments'] as const;
export const shipments = {
	all: (params: All) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'all', params],
			queryFn: async () => (await api.all(params)).data,
		}),
	sortings: queryOptions({
		queryKey: [...BASE_KEY, 'sortings'],
		queryFn: async () => (await api.sortings()).data,
	}),
	waybill: (params: Waybill) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'waybill', params],
			queryFn: async () => (await api.waybill(params)).data,
		}),
	track: (params: Track) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'track', params],
			queryFn: async () => (await api.track(params)).data,
		}),
};
