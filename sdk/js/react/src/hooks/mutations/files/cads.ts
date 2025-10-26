import { mutationOptions } from '@tanstack/react-query';
import { cadsApi as api } from '@/api';
import * as presigned from '@/api/files/presigned';
import { Request as Create } from '@/api/files/cads/create';
import { Request as Edit } from '@/api/files/cads/edit';
import { Request as Coords } from '@/api/files/cads/coords';

const BASE_KEY = ['cads'];
export const cads = {
	create: mutationOptions({
		mutationKey: [...BASE_KEY, 'create'],
		mutationFn: async (params: Create) => (await api.create(params)).data,
	}),
	edit: mutationOptions({
		mutationKey: [...BASE_KEY, 'edit'],
		mutationFn: async (params: Edit) => (await api.edit(params)).data,
	}),
	coords: mutationOptions({
		mutationKey: [...BASE_KEY, 'coords'],
		mutationFn: async (params: Coords) =>
			(await api.setCoords(params)).data,
	}),
	upload: mutationOptions({
		mutationKey: [...BASE_KEY, 'upload'],
		mutationFn: async (params: presigned.UploadRequest) =>
			(await api.uploadUrl(params)).data,
	}),
	replace: mutationOptions({
		mutationKey: [...BASE_KEY, 'replace'],
		mutationFn: async (params: presigned.ReplaceRequest) =>
			(await api.replaceUrl(params)).data,
	}),
	download: mutationOptions({
		mutationKey: [...BASE_KEY, 'download'],
		mutationFn: async (params: presigned.DownloadRequest) =>
			(await api.downloadUrl(params)).data,
	}),
};
