import { mutationOptions } from '@tanstack/react-query';
import { products as api } from '@/api';
import { Request as CreatorCreate } from '@/api/catalog/products/creator/create';
import { Request as CreatorEdit } from '@/api/catalog/products/creator/edit';
import { Request as CreatorDelete } from '@/api/catalog/products/creator/delete';
import { Request as DesignerValidate } from '@/api/catalog/products/designer/validate';
import { Request as DesignerReport } from '@/api/catalog/products/designer/report';
import { Request as AdminRemove } from '@/api/catalog/products/admin/remove';
import { Request as AdminAddTag } from '@/api/catalog/products/admin/add-tag';
import { Request as AdminRemoveTag } from '@/api/catalog/products/admin/remove-tag';

const CREATOR_BASE_KEY = ['products', 'creator'] as const;
export const creator = {
	create: mutationOptions({
		mutationKey: [...CREATOR_BASE_KEY, 'create'],
		mutationFn: async (params: CreatorCreate) =>
			(await api.creator.create(params)).data,
	}),
	edit: mutationOptions({
		mutationKey: [...CREATOR_BASE_KEY, 'edit'],
		mutationFn: async (params: CreatorEdit) =>
			(await api.creator.edit(params)).data,
	}),
	delete: mutationOptions({
		mutationKey: [...CREATOR_BASE_KEY, 'delete'],
		mutationFn: async (params: CreatorDelete) =>
			(await api.creator.delete_(params)).data,
	}),
};

const DESIGNER_BASE_KEY = ['products', 'designer'] as const;
export const designer = {
	validate: mutationOptions({
		mutationKey: [...DESIGNER_BASE_KEY, 'validate'],
		mutationFn: async (params: DesignerValidate) =>
			(await api.designer.validate(params)).data,
	}),
	report: mutationOptions({
		mutationKey: [...DESIGNER_BASE_KEY, 'report'],
		mutationFn: async (params: DesignerReport) =>
			(await api.designer.report(params)).data,
	}),
};

const ADMIN_BASE_KEY = ['products', 'admin'] as const;
export const admin = {
	remove: mutationOptions({
		mutationKey: [...ADMIN_BASE_KEY, 'remove'],
		mutationFn: async (params: AdminRemove) =>
			(await api.admin.remove(params)).data,
	}),
	addTag: mutationOptions({
		mutationKey: [...ADMIN_BASE_KEY, 'addTag'],
		mutationFn: async (params: AdminAddTag) =>
			(await api.admin.addTag(params)).data,
	}),
	removeTag: mutationOptions({
		mutationKey: [...ADMIN_BASE_KEY, 'removeTag'],
		mutationFn: async (params: AdminRemoveTag) =>
			(await api.admin.removeTag(params)).data,
	}),
};
