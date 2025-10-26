import { mutationOptions } from '@tanstack/react-query';
import { notificationsApi as api } from '@/api';
import { Request as Read } from '@/api/notifications/notifications/read';
import { Request as Open } from '@/api/notifications/notifications/open';
import { Request as Hide } from '@/api/notifications/notifications/hide';

const BASE_KEY = ['notifications'] as const;
export const notifications = {
	read: mutationOptions({
		mutationKey: [BASE_KEY, 'read'],
		mutationFn: async (params: Read) => (await api.read(params)).data,
	}),
	open: mutationOptions({
		mutationKey: [BASE_KEY, 'open'],
		mutationFn: async (params: Open) => (await api.open(params)).data,
	}),
	hide: mutationOptions({
		mutationKey: [BASE_KEY, 'hide'],
		mutationFn: async (params: Hide) => (await api.hide(params)).data,
	}),
};
