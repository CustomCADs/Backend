import { IDENTITY_BASE_PATH } from '../common';

export type Request = {
	productId: string;
};

export const url = () => `${IDENTITY_BASE_PATH}/viewed-product`;
