import * as headers from '@/api/common/headers';
import { CATEGORY_BASE_PATH } from '../common';

export type Request = {
	name: string;
	description: string;
} & headers.IdempotencyKey;

export const url = () => `${CATEGORY_BASE_PATH}`;
