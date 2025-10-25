import { mutationOptions } from '@tanstack/react-query';
import { customizations as api } from '@/api';
import { Request as Create } from '@/api/printing/customizations/create';
import { Request as Edit } from '@/api/printing/customizations/edit';

const BASE_KEY = ['customizations'] as const;
export const customizations = {
	create: mutationOptions({
		mutationKey: [...BASE_KEY, 'create'],
		mutationFn: async (params: Create) => (await api.create(params)).data,
	}),
	edit: mutationOptions({
		mutationKey: [...BASE_KEY, 'edit'],
		mutationFn: async (params: Edit) => (await api.edit(params)).data,
	}),
};
