import {
	UseMutationOptions,
	useMutation as useTanstackMutation,
} from '@tanstack/react-query';
import { src } from '../mutations';

export const useMutation = <TRequest, TResponse>(
	optionsSelector: (
		mutation: typeof src,
	) => UseMutationOptions<TResponse, Error, TRequest, unknown>,
) => useTanstackMutation(optionsSelector(src));
