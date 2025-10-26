import * as customcadsQueries from './';
import {
	UseQueryOptions,
	useQuery as useTanstackQuery,
	UseInfiniteQueryOptions,
	useInfiniteQuery as useTanstackInfiniteQuery,
	InfiniteData,
} from '@tanstack/react-query';

export const useQuery = <TData, TKey extends readonly unknown[]>(
	optionsSelector: (
		queries: typeof customcadsQueries,
	) => UseQueryOptions<TData, Error, TData, TKey>,
	enabled?: boolean,
) => useTanstackQuery({ ...optionsSelector(customcadsQueries), enabled });

export const useInfiniteQuery = <TData, TKey extends readonly unknown[]>(
	optionsSelector: (
		queries: typeof customcadsQueries,
	) => UseInfiniteQueryOptions<
		TData,
		Error,
		InfiniteData<TData, unknown>,
		TKey,
		number
	>,
	enabled?: boolean,
) =>
	useTanstackInfiniteQuery({
		...optionsSelector(customcadsQueries),
		enabled,
	});
