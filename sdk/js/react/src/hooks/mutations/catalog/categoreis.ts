import { mutationOptions } from '@tanstack/react-query';
import { categories as api } from '@/api';
import { Request as Create } from '@/api/catalog/categories/create';
import { Request as Edit } from '@/api/catalog/categories/edit';
import { Request as Delete } from '@/api/catalog/categories/delete';

const BASE_KEY = ['categories'] as const;
export const categories = {
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
