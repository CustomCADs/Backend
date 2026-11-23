import { queryOptions } from '@tanstack/react-query';
import { shipmentsApi as api } from '@/api';
import { Request as All } from '@/api/delivery/shipments/all';
import { Request as Single } from '@/api/delivery/shipments/single';
import { Request as Reference } from '@/api/delivery/shipments/reference';
import { Request as Country } from '@/api/delivery/shipments/country';
import { Request as Waybill } from '@/api/delivery/shipments/waybill';
import { Request as Track } from '@/api/delivery/shipments/track';

const BASE_KEY = ['shipments'] as const;
export const shipments = {
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
	reference: (params: Reference) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'reference', params] as const,
			queryFn: () => api.reference(params),
		}),
	country: (params: Country) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'country', params] as const,
			queryFn: () => api.country(params),
		}),
	sortings: queryOptions({
		queryKey: [...BASE_KEY, 'sortings'] as const,
		queryFn: api.sortings,
	}),
	waybill: (params: Waybill) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'waybill', params] as const,
			queryFn: () => api.waybill(params),
		}),
	track: (params: Track) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'track', params] as const,
			queryFn: () => api.track(params),
		}),
};
