import { AxiosResponse } from 'axios';
import {
	UseSuspenseQueryOptions,
	useSuspenseQuery as useTanstackSuspenseQuery,
} from '@tanstack/react-query';
import { src } from '../queries';

export const useSuspenseQuery = <TData, TKey extends readonly unknown[]>(
	optionsSelector: (
		queries: typeof src,
	) => UseSuspenseQueryOptions<
		AxiosResponse<TData>,
		Error,
		AxiosResponse<TData>,
		TKey
	>,
) =>
	useTanstackSuspenseQuery({
		...optionsSelector(src),
		select: ({ data }) => data,
	});
