import { AxiosResponse } from 'axios';
import * as customcadsQueries from './';
import {
	UseQueryOptions,
	useQuery as useTanstackQuery,
} from '@tanstack/react-query';

export const useQuery = <TData, TKey extends readonly unknown[]>(
	optionsSelector: (
		queries: typeof customcadsQueries,
	) => UseQueryOptions<
		AxiosResponse<TData>,
		Error,
		AxiosResponse<TData>,
		TKey
	>,
	enabled?: boolean,
) =>
	useTanstackQuery({
		...optionsSelector(customcadsQueries),
		enabled,
		select: ({ data }) => data,
	});
