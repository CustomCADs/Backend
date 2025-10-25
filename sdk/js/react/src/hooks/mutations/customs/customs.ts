import { mutationOptions } from '@tanstack/react-query';
import { customs as api } from '@/api';
import { Request as CustomerCreate } from '@/api/customs/customs/customer/create';
import { Request as CustomerEdit } from '@/api/customs/customs/customer/edit';
import { Request as CustomerPurchase } from '@/api/customs/customs/customer/purchase';
import { Request as CustomerPurchaseDelivery } from '@/api/customs/customs/customer/purchase-delivery';
import { Request as CustomerDelete } from '@/api/customs/customs/customer/delete';
import { Request as DesignerSetCategory } from '@/api/customs/customs/designer/category';
import { Request as DesignerStatus } from '@/api/customs/customs/designer/status';
import { Request as DesignerFinish } from '@/api/customs/customs/designer/finish';
import { Request as AdminSetCategory } from '@/api/customs/customs/admin/category';
import { Request as AdminStatus } from '@/api/customs/customs/admin/status';

const CUSTOMERS_BASE_KEY = ['customs', 'customer'] as const;
export const customers = {
	create: mutationOptions({
		mutationKey: [...CUSTOMERS_BASE_KEY, 'create'],
		mutationFn: async (params: CustomerCreate) =>
			(await api.customer.create(params)).data,
	}),
	edit: mutationOptions({
		mutationKey: [...CUSTOMERS_BASE_KEY, 'edit'],
		mutationFn: async (params: CustomerEdit) =>
			(await api.customer.edit(params)).data,
	}),
	purchase: mutationOptions({
		mutationKey: [...CUSTOMERS_BASE_KEY, 'purchase'],
		mutationFn: async (params: CustomerPurchase) =>
			(await api.customer.purchase(params)).data,
	}),
	purchaseWithDelivery: mutationOptions({
		mutationKey: [...CUSTOMERS_BASE_KEY, 'purchase-delivery'],
		mutationFn: async (params: CustomerPurchaseDelivery) =>
			(await api.customer.purchaseDelivery(params)).data,
	}),
	delete: mutationOptions({
		mutationKey: [...CUSTOMERS_BASE_KEY, 'delete'],
		mutationFn: async (params: CustomerDelete) =>
			(await api.customer.delete_(params)).data,
	}),
};

const DESIGNERS_BASE_KEY = ['customs', 'designer'] as const;
export const designers = {
	cateogry: mutationOptions({
		mutationKey: [...DESIGNERS_BASE_KEY, 'set-category'],
		mutationFn: async (params: DesignerSetCategory) =>
			(await api.designer.setCategory(params)).data,
	}),
	accept: mutationOptions({
		mutationKey: [...DESIGNERS_BASE_KEY, 'accept'],
		mutationFn: async (params: DesignerStatus) =>
			(await api.designer.accept(params)).data,
	}),
	begin: mutationOptions({
		mutationKey: [...DESIGNERS_BASE_KEY, 'begin'],
		mutationFn: async (params: DesignerStatus) =>
			(await api.designer.begin(params)).data,
	}),
	cancel: mutationOptions({
		mutationKey: [...DESIGNERS_BASE_KEY, 'cancel'],
		mutationFn: async (params: DesignerStatus) =>
			(await api.designer.cancel(params)).data,
	}),
	report: mutationOptions({
		mutationKey: [...DESIGNERS_BASE_KEY, 'report'],
		mutationFn: async (params: DesignerStatus) =>
			(await api.designer.report(params)).data,
	}),
	finish: mutationOptions({
		mutationKey: [...DESIGNERS_BASE_KEY, 'finish'],
		mutationFn: async (params: DesignerFinish) =>
			(await api.designer.finish(params)).data,
	}),
};

const ADMINS_BASE_KEY = ['customs', 'admin'] as const;
export const keys = {
	category: mutationOptions({
		mutationKey: [...ADMINS_BASE_KEY, 'set-category'],
		mutationFn: async (params: AdminSetCategory) =>
			(await api.admin.setCategory(params)).data,
	}),
	remove: mutationOptions({
		mutationKey: [...ADMINS_BASE_KEY, 'remove'],
		mutationFn: async (params: AdminStatus) =>
			(await api.admin.remove(params)).data,
	}),
};
