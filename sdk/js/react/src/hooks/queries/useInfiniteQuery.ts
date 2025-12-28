import {
	UseInfiniteQueryOptions,
	useInfiniteQuery as useTanstackInfiniteQuery,
	InfiniteData,
} from '@tanstack/react-query';
import * as customcadsQueries from './';

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
