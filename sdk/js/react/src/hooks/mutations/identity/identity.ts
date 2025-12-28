import { mutationOptions } from '@tanstack/react-query';
import { identityApi as api } from '@/api';
import { Request as Login } from '@/api/identity/identity/login';
import { Request as ForgotPassword } from '@/api/identity/identity/forgot-password';
import { Request as ResetPassword } from '@/api/identity/identity/reset-password';
import { Request as ChangeUsername } from '@/api/identity/identity/change-username';
import { Request as Register } from '@/api/identity/identity/register';
import { Request as ConfirmEmail } from '@/api/identity/identity/confirm-email';
import { Request as RetryConfirmEmail } from '@/api/identity/identity/retry-confirm-email';

const BASE_KEY = ['identity'] as const;
export const identity = {
	login: mutationOptions({
		mutationKey: [...BASE_KEY, 'login'],
		mutationFn: async (params: Login) => (await api.login(params)).data,
	}),
	logout: mutationOptions({
		mutationKey: [...BASE_KEY, 'logout'],
		mutationFn: async () => (await api.logout()).data,
	}),
	refresh: mutationOptions({
		mutationKey: [...BASE_KEY, 'refresh'],
		mutationFn: async () => (await api.refresh()).data,
	}),
	changeUsername: mutationOptions({
		mutationKey: [...BASE_KEY, 'change-username'],
		mutationFn: async (params: ChangeUsername) =>
			(await api.changeUsername(params)).data,
	}),
	toggleTrackViewedProducts: mutationOptions({
		mutationKey: [...BASE_KEY, 'toggle-track-viewed-products'],
		mutationFn: async () => (await api.toggleTrackViewedProducts()).data,
	}),
	deleteMyAccount: mutationOptions({
		mutationKey: [...BASE_KEY, 'delete-my-account'],
		mutationFn: async () => (await api.delete_()).data,
	}),
	forgotPassword: mutationOptions({
		mutationKey: [...BASE_KEY, 'forgot-password'],
		mutationFn: async (params: ForgotPassword) =>
			(await api.forgotPassword(params)).data,
	}),
	resetPassword: mutationOptions({
		mutationKey: [...BASE_KEY, 'reset-password'],
		mutationFn: async (params: ResetPassword) =>
			(await api.resetPassword(params)).data,
	}),
	register: mutationOptions({
		mutationKey: [...BASE_KEY, 'register'],
		mutationFn: async (params: Register) =>
			(await api.register(params)).data,
	}),
	confirmEmail: mutationOptions({
		mutationKey: [...BASE_KEY, 'confirm-email'],
		mutationFn: async (params: ConfirmEmail) =>
			(await api.confirmEmail(params)).data,
	}),
	retryConfirmEmail: mutationOptions({
		mutationKey: [...BASE_KEY, 'retry-confirm-email'],
		mutationFn: async (params: RetryConfirmEmail) =>
			(await api.retryConfirmEmail(params)).data,
	}),
};
