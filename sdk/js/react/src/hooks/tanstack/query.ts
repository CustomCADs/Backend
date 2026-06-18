import { AxiosResponse } from 'axios';
import { FetchQueryOptions, QueryClient } from '@tanstack/react-query';
import { src } from '../queries';

type Opts<TData, TKey extends readonly unknown[]> = FetchQueryOptions<
	AxiosResponse<TData>,
	Error,
	AxiosResponse<TData>,
	TKey
>;

export const query = {
	fetchQuery: <TData, TKey extends readonly unknown[]>(
		optionsSelector: (queries: typeof src) => Opts<TData, TKey>,
		queryClient: QueryClient,
	) => queryClient.fetchQuery(optionsSelector(src)),
	prefetchQuery: <TData, TKey extends readonly unknown[]>(
		optionsSelector: (queries: typeof src) => Opts<TData, TKey>,
		queryClient: QueryClient,
	) => queryClient.prefetchQuery(optionsSelector(src)),
	invalidateQueries: <TData, TKey extends readonly unknown[]>(
		optionsSelector: (queries: typeof src) => Opts<TData, TKey>,
		queryClient: QueryClient,
	) => queryClient.invalidateQueries(optionsSelector(src)),
};
