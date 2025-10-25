import { objectToSearchParams } from '@/utils/params';
import { IMAGES_BASE_PATH } from '../common';
import { DownloadBulkRequest, DownloadBulkResponse } from '../presigned';

export type Request = DownloadBulkRequest;
export type Response = DownloadBulkResponse;

export const url = (req: Request) =>
	`${IMAGES_BASE_PATH}?${objectToSearchParams(req)}`;
