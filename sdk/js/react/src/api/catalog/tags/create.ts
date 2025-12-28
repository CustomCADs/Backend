import * as headers from '@/api/common/headers';
import { TAG_BASE_PATH } from '../common';

export type Request = {
	name: string;
} & headers.IdempotencyKey;

export const url = () => `${TAG_BASE_PATH}`;
