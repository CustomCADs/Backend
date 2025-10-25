import { mutationOptions } from '@tanstack/react-query';
import { materials as api } from '@/api';
import { Request as Create } from '@/api/printing/materials/create';
import { Request as Edit } from '@/api/printing/materials/edit';
import { Request as Delete } from '@/api/printing/materials/delete';

const BASE_KEY = ['materials'] as const;
export const materials = {
	create: mutationOptions({
		mutationKey: [BASE_KEY, 'create'],
		mutationFn: async (params: Create) => (await api.create(params)).data,
	}),
	edit: mutationOptions({
		mutationKey: [BASE_KEY, 'edit'],
		mutationFn: async (params: Edit) => (await api.edit(params)).data,
	}),
	delete: mutationOptions({
		mutationKey: [BASE_KEY, 'delete'],
		mutationFn: async (params: Delete) => (await api.delete_(params)).data,
	}),
};
