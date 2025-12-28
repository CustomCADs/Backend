import { queryOptions } from '@tanstack/react-query';
import { customizationsApi as api } from '@/api';
import { Request as Single } from '@/api/printing/customizations/single';

const BASE_KEY = ['customizations'] as const;
export const customizations = {
	single: (params: Single) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params] as const,
			queryFn: () => api.single(params),
		}),
};
