import * as headers from '@/api/common/headers';
import { CUSTOMIZATIONS_BASE_PATH } from '@//api/printing/common';

export type Request = {
	infill: number;
	volume: number;
	base: number;
	scale: number;
	color: string;
	materialId: number;
} & headers.IdempotencyKey;

export const url = () => `${CUSTOMIZATIONS_BASE_PATH}`;
