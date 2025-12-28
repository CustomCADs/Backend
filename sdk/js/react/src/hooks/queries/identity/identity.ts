import { queryOptions } from '@tanstack/react-query';
import { identityApi as api } from '@/api';

const BASE_KEY = ['identity'] as const;
export const identity = {
	authn: queryOptions({
		queryKey: [...BASE_KEY, 'authn'] as const,
		queryFn: api.authn,
	}),
	authz: queryOptions({
		queryKey: [...BASE_KEY, 'authz'] as const,
		queryFn: api.authz,
	}),
	myAccount: queryOptions({
		queryKey: [...BASE_KEY, 'my-account'] as const,
		queryFn: api.myAccount,
	}),
	downloadInfo: queryOptions({
		queryKey: [...BASE_KEY, 'download-info'] as const,
		queryFn: api.downloadInfo,
	}),
};
