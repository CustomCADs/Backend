import { IMAGES_BASE_PATH } from '../common';
import { DownloadBulkRequest, DownloadBulkResponse } from '../presigned';

export type Request = DownloadBulkRequest;
export type Response = DownloadBulkResponse;

export const url = () => `${IMAGES_BASE_PATH}/presigned/bulk-download`;
