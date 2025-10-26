import { queryOptions } from '@tanstack/react-query';
import { identityApi as api } from '@/api';

const BASE_KEY = ['identity'] as const;
export const identity = {
	authn: queryOptions({
		queryKey: [...BASE_KEY, 'authn'],
		queryFn: api.authn,
	}),
	authz: queryOptions({
		queryKey: [...BASE_KEY, 'authz'],
		queryFn: api.authz,
	}),
	myAccount: queryOptions({
		queryKey: [...BASE_KEY, 'my-account'],
		queryFn: api.myAccount,
	}),
	downloadInfo: queryOptions({
		queryKey: [...BASE_KEY, 'download-info'],
		queryFn: api.downloadInfo,
	}),
};
