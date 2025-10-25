import * as customcadsMutations from './';
import {
	UseMutationOptions,
	useMutation as useTanstackMutation,
} from '@tanstack/react-query';

export const useMutation = <TRequest, TResponse>(
	optionsSelector: (
		mutation: typeof customcadsMutations,
	) => UseMutationOptions<TResponse, Error, TRequest, unknown>,
) => useTanstackMutation(optionsSelector(customcadsMutations));
