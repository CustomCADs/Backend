import { queryOptions } from '@tanstack/react-query';
import { customsApi as api } from '@/api';
import { Request as CustomerAll } from '@/api/customs/customs/customer/all';
import { Request as CustomerSingle } from '@/api/customs/customs/customer/single';
import { Request as CustomerRecent } from '@/api/customs/customs/customer/recent';
import { Request as CustomerCalculate } from '@/api/customs/customs/customer/calculate-shipment';
import { Request as DesignerAll } from '@/api/customs/customs/designer/all';
import { Request as DesignerSingle } from '@/api/customs/customs/designer/single';
import { Request as AdminAll } from '@/api/customs/customs/admin/all';

const CUSTOMER_BASE_KEY = ['customs', 'customer'] as const;
export const customer = {
	all: (params: CustomerAll) =>
		queryOptions({
			queryKey: [CUSTOMER_BASE_KEY, 'all', params],
			queryFn: async () => (await api.customer.all(params)).data,
		}),
	single: (params: CustomerSingle) =>
		queryOptions({
			queryKey: [CUSTOMER_BASE_KEY, 'single', params],
			queryFn: async () => (await api.customer.single(params)).data,
		}),
	recent: (params: CustomerRecent) =>
		queryOptions({
			queryKey: [CUSTOMER_BASE_KEY, 'recent', params],
			queryFn: async () => (await api.customer.recent(params)).data,
		}),
	stats: queryOptions({
		queryKey: [CUSTOMER_BASE_KEY, 'stats'],
		queryFn: async () => (await api.customer.stats()).data,
	}),
	sortings: queryOptions({
		queryKey: [CUSTOMER_BASE_KEY, 'sortings'],
		queryFn: async () => (await api.customer.sortings()).data,
	}),
	paymentStatuses: queryOptions({
		queryKey: [CUSTOMER_BASE_KEY, 'payment-statuses'],
		queryFn: async () => (await api.customer.paymentStatuses()).data,
	}),
	calculateShipment: (params: CustomerCalculate) =>
		queryOptions({
			queryKey: [CUSTOMER_BASE_KEY, 'calculate-shipment', params],
			queryFn: async () =>
				(await api.customer.calculateShipment(params)).data,
		}),
};

const DESIGNER_BASE_KEY = ['customs', 'designer'] as const;
export const designer = {
	all: (params: DesignerAll) =>
		queryOptions({
			queryKey: [DESIGNER_BASE_KEY, 'all', params],
			queryFn: async () => (await api.designer.all(params)).data,
		}),
	single: (params: DesignerSingle) =>
		queryOptions({
			queryKey: [DESIGNER_BASE_KEY, 'single', params],
			queryFn: async () => (await api.designer.single(params)).data,
		}),
};

const ADMIN_BASE_KEY = ['customs', 'admin'] as const;
export const admin = {
	all: (params: AdminAll) =>
		queryOptions({
			queryKey: [ADMIN_BASE_KEY, 'all', params],
			queryFn: async () => (await api.admin.all(params)).data,
		}),
};
