import { mutationOptions } from '@tanstack/react-query';
import { roles as api } from '@/api';
import { Request as Create } from '@/api/accounts/roles/create';
import { Request as Delete } from '@/api/accounts/roles/delete';

const BASE_KEY = ['roles'] as const;
export const roles = {
	create: mutationOptions({
		mutationKey: [...BASE_KEY, 'create'],
		mutationFn: async (params: Create) => (await api.create(params)).data,
	}),
	delete: mutationOptions({
		mutationKey: [...BASE_KEY, 'delete'],
		mutationFn: async (params: Delete) => (await api.delete_(params)).data,
	}),
};
