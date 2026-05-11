import { IDENTITY_BASE_PATH } from '../common';

export type Request = {
	refreshTokenId: string;
};

export const url = () => `${IDENTITY_BASE_PATH}/fingerprint`;
