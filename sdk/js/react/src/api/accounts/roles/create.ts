import * as headers from '@/api/common/headers';
import { ROLES_BASE_PATH } from '../common';

export type Request = {
	name: string;
	description: string;
} & headers.IdempotencyKey;

export const url = () => `${ROLES_BASE_PATH}`;
