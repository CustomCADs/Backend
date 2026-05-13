import { AxiosResponse } from 'axios';
import { FetchQueryOptions, QueryClient } from '@tanstack/react-query';
import * as customcadsQueries from '.';

type Opts<TData, TKey extends readonly unknown[]> = FetchQueryOptions<
	AxiosResponse<TData>,
	Error,
	AxiosResponse<TData>,
	TKey
>;

export const query = {
	fetchQuery: <TData, TKey extends readonly unknown[]>(
		optionsSelector: (
			queries: typeof customcadsQueries,
		) => Opts<TData, TKey>,
		queryClient: QueryClient,
	) => queryClient.fetchQuery(optionsSelector(customcadsQueries)),
	invalidateQueries: <TData, TKey extends readonly unknown[]>(
		optionsSelector: (
			queries: typeof customcadsQueries,
		) => Opts<TData, TKey>,
		queryClient: QueryClient,
	) => queryClient.invalidateQueries(optionsSelector(customcadsQueries)),
};
