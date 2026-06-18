import { AxiosResponse } from 'axios';
import {
	UseQueryOptions,
	useQuery as useTanstackQuery,
} from '@tanstack/react-query';
import { src } from '../queries';

export const useQuery = <TData, TKey extends readonly unknown[]>(
	optionsSelector: (
		queries: typeof src,
	) => UseQueryOptions<
		AxiosResponse<TData>,
		Error,
		AxiosResponse<TData>,
		TKey
	>,
	enabled?: boolean,
) =>
	useTanstackQuery({
		...optionsSelector(src),
		enabled,
		select: ({ data }) => data,
	});
