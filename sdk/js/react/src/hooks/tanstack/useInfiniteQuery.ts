import {
	UseInfiniteQueryOptions,
	useInfiniteQuery as useTanstackInfiniteQuery,
	InfiniteData,
} from '@tanstack/react-query';
import { src } from '../queries';

export const useInfiniteQuery = <TData, TKey extends readonly unknown[]>(
	optionsSelector: (
		queries: typeof src,
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
		...optionsSelector(src),
		enabled,
	});
