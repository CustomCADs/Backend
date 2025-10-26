import { queryOptions } from '@tanstack/react-query';
import { imagesApi as api } from '@/api';
import { DownloadRequest } from '@/api/files/presigned';
import { Request as Single } from '@/api/files/images/single';

const BASE_KEY = ['images'] as const;
export const images = {
	single: (params: Single) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'single', params],
			queryFn: () => api.single(params),
		}),
	download: (params: DownloadRequest) =>
		queryOptions({
			queryKey: [...BASE_KEY, 'download', params],
			queryFn: () => api.downloadUrl(params),
		}),
};
