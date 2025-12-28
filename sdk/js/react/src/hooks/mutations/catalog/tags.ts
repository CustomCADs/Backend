import { mutationOptions } from '@tanstack/react-query';
import { tagsApi as api } from '@/api';
import { Request as Create } from '@/api/catalog/tags/create';
import { Request as Edit } from '@/api/catalog/tags/edit';
import { Request as Delete } from '@/api/catalog/tags/delete';

const BASE_KEY = ['tags'] as const;
export const tags = {
	create: mutationOptions({
		mutationKey: [...BASE_KEY, 'create'],
		mutationFn: async (params: Create) => (await api.create(params)).data,
	}),
	edit: mutationOptions({
		mutationKey: [...BASE_KEY, 'edit'],
		mutationFn: async (params: Edit) => (await api.edit(params)).data,
	}),
	delete: mutationOptions({
		mutationKey: [...BASE_KEY, 'delete'],
		mutationFn: async (params: Delete) => (await api.delete_(params)).data,
	}),
};
