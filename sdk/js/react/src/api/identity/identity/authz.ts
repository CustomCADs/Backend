import { Role } from '@/api/common';
import { IDENTITY_BASE_PATH } from '../common';

export type Response = Role;

export const url = () => `${IDENTITY_BASE_PATH}/authorization`;
