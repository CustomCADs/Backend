import { mutationOptions } from '@tanstack/react-query';
import { images as api } from '@/api';
import {
	UploadRequest,
	ReplaceRequest,
	DownloadRequest,
	DownloadBulkRequest,
} from '@/api/files/presigned';
import { Request as Create } from '@/api/files/images/create';
import { Request as Edit } from '@/api/files/images/edit';

const BASE_KEY = ['images'] as const;
export const images = {
	create: mutationOptions({
		mutationKey: [...BASE_KEY, 'create'],
		mutationFn: async (params: Create) => (await api.create(params)).data,
	}),
	edit: mutationOptions({
		mutationKey: [...BASE_KEY, 'edit'],
		mutationFn: async (params: Edit) => (await api.edit(params)).data,
	}),
	bulkDownload: mutationOptions({
		mutationKey: [...BASE_KEY, 'bulk-download'],
		mutationFn: async (params: DownloadBulkRequest) =>
			(await api.bulkDownloadUrls(params)).data,
	}),
	download: mutationOptions({
		mutationKey: [...BASE_KEY, 'download'],
		mutationFn: async (params: DownloadRequest) =>
			(await api.downloadUrl(params)).data,
	}),
	upload: mutationOptions({
		mutationKey: [...BASE_KEY, 'upload'],
		mutationFn: async (params: UploadRequest) =>
			(await api.uploadUrl(params)).data,
	}),
	replace: mutationOptions({
		mutationKey: [...BASE_KEY, 'replace'],
		mutationFn: async (params: ReplaceRequest) =>
			(await api.replaceUrl(params)).data,
	}),
};
