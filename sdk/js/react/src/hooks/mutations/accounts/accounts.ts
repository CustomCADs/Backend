import { mutationOptions } from '@tanstack/react-query';
import { accounts as api } from '@/api';
import { Request as Create } from '@/api/accounts/accounts/create';
import { Request as Delete } from '@/api/accounts/accounts/delete';

const BASE_KEY = ['accounts'] as const;
export const accounts = {
	create: mutationOptions({
		mutationKey: [...BASE_KEY, 'create'],
		mutationFn: async (params: Create) => (await api.create(params)).data,
	}),
	delete: mutationOptions({
		mutationKey: [...BASE_KEY, 'delete'],
		mutationFn: async (params: Delete) => (await api.delete_(params)).data,
	}),
};
