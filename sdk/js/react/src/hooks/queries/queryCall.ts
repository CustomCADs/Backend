import { AxiosResponse } from 'axios';
import { FetchQueryOptions } from '@tanstack/react-query';
import * as customcadsQueries from '.';

type Opts<TData, TKey extends readonly unknown[]> = FetchQueryOptions<
	AxiosResponse<TData>,
	Error,
	AxiosResponse<TData>,
	TKey
>;

export const queryCall = <TData, TKey extends readonly unknown[]>(
	optionsSelector: (queries: typeof customcadsQueries) => Opts<TData, TKey>,
	call: (options: Opts<TData, TKey>) => Promise<AxiosResponse<TData> | void>,
) => call(optionsSelector(customcadsQueries));
