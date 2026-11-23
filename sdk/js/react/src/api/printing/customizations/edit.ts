import { CUSTOMIZATIONS_BASE_PATH } from '@//api/printing/common';

export type Request = {
	id: string;
	infill: number;
	volume: number;
	base: number;
	scale: number;
	color: string;
	materialId: number;
};

export const url = () => `${CUSTOMIZATIONS_BASE_PATH}`;
