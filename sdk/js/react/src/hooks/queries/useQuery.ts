import * as customcadsQueries from './';
import {
	UseQueryOptions,
	useQuery as useTanstackQuery,
} from '@tanstack/react-query';

export const useQuery = <TData, TKey extends readonly unknown[]>(
	optionsSelector: (
		queries: typeof customcadsQueries,
	) => UseQueryOptions<TData, Error, TData, TKey>,
	enabled?: boolean,
) => useTanstackQuery({ ...optionsSelector(customcadsQueries), enabled });
