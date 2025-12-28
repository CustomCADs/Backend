import * as headers from '@/api/common/headers';
import { ACCOUNTS_BASE_PATH } from '../common';

export type Request = {
	role: string;
	username: string;
	email: string;
	password: string;
	firstName?: string;
	lastName?: string;
} & headers.IdempotencyKey;

export const url = () => `${ACCOUNTS_BASE_PATH}`;
