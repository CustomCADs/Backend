import { queryOptions } from '@tanstack/react-query';
import { shipmentsApi as api } from '@/api';
import { Request as All } from '@/api/delivery/shipments/all';
import { Request as Waybill } from '@/api/delivery/shipments/waybill';
import { Request as Track } from '@/api/delivery/shipments/track';

const BASE_KEY = ['shipments'] as const;
export const shipments = {
	all: (params: All) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'all', params],
			queryFn: () => api.all(params),
		}),
	sortings: queryOptions({
		queryKey: [...BASE_KEY, 'sortings'],
		queryFn: api.sortings,
	}),
	waybill: (params: Waybill) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'waybill', params],
			queryFn: () => api.waybill(params),
		}),
	track: (params: Track) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'track', params],
			queryFn: () => api.track(params),
		}),
};
