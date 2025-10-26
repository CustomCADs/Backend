import { queryOptions } from '@tanstack/react-query';
import { identityApi as api } from '@/api';

const BASE_KEY = ['identity'] as const;
export const identity = {
	authn: queryOptions({
		queryKey: [...BASE_KEY, 'authn'],
		queryFn: async () => (await api.authn()).data,
	}),
	authz: queryOptions({
		queryKey: [...BASE_KEY, 'authz'],
		queryFn: async () => (await api.authz()).data,
	}),
	myAccount: queryOptions({
		queryKey: [...BASE_KEY, 'my-account'],
		queryFn: async () => (await api.myAccount()).data,
	}),
	downloadInfo: queryOptions({
		queryKey: [...BASE_KEY, 'download-info'],
		queryFn: async () => (await api.downloadInfo()).data,
	}),
};
