import { queryOptions } from '@tanstack/react-query';
import { images as api } from '@/api';
import { Request as Single } from '@/api/files/images/single';

const BASE_KEY = ['images'] as const;
export const images = {
	single: (params: Single) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params],
			queryFn: async () => (await api.single(params)).data,
		}),
};
