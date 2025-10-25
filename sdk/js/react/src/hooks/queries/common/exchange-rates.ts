import { queryOptions } from '@tanstack/react-query';
import * as api from '@/api/common/exchange-rates';

const BASE_KEY = ['common'] as const;
export const exchangeRates = {
	all: queryOptions({
		queryKey: [...BASE_KEY, 'all'],
		queryFn: async () => (await api.all()).data,
	}),
};
