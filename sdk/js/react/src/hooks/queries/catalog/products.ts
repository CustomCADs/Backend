import { queryOptions } from '@tanstack/react-query';
import { products } from '@/api';
import { Request as GalleryAll } from '@/api/catalog/products/gallery/all';
import { Request as GallerySingle } from '@/api/catalog/products/gallery/single';
import { Request as CreatorAll } from '@/api/catalog/products/creator/all';
import { Request as CreatorSingle } from '@/api/catalog/products/creator/single';
import { Request as CreatorRecent } from '@/api/catalog/products/creator/recent';
import { Request as DesignerAll } from '@/api/catalog/products/designer/all';
import { Request as DesignerSingle } from '@/api/catalog/products/designer/single';

const GALLERY_BASE_KEY = ['products', 'gallery'] as const;
export const gallery = {
	all: (params: GalleryAll) =>
		queryOptions({
			queryKey: [...GALLERY_BASE_KEY, 'all', params],
			queryFn: async () => (await products.gallery.all(params)).data,
		}),
	single: (params: GallerySingle) =>
		queryOptions({
			queryKey: [...GALLERY_BASE_KEY, 'single', params],
			queryFn: async () => (await products.gallery.single(params)).data,
		}),
	sortings: queryOptions({
		queryKey: [...GALLERY_BASE_KEY, 'sortings'],
		queryFn: async () => (await products.gallery.sortings()).data,
	}),
};

const CREATOR_BASE_KEY = ['products', 'creator'] as const;
export const creator = {
	all: (params: CreatorAll) =>
		queryOptions({
			queryKey: [...CREATOR_BASE_KEY, 'all', params],
			queryFn: async () => (await products.creator.all(params)).data,
		}),
	single: (params: CreatorSingle) =>
		queryOptions({
			queryKey: [...CREATOR_BASE_KEY, 'single', params],
			queryFn: async () => (await products.creator.single(params)).data,
		}),
	recent: (params: CreatorRecent) =>
		queryOptions({
			queryKey: [...CREATOR_BASE_KEY, 'recent'],
			queryFn: async () => (await products.creator.recent(params)).data,
		}),
	stats: queryOptions({
		queryKey: [...CREATOR_BASE_KEY, 'stats'],
		queryFn: async () => (await products.creator.stats()).data,
	}),
};

const DESIGNER_BASE_KEY = ['products', 'designer'] as const;
export const designer = {
	unchecked: (params: DesignerAll) =>
		queryOptions({
			queryKey: [...DESIGNER_BASE_KEY, 'unchecked', params],
			queryFn: async () =>
				(await products.designer.unchecked(params)).data,
		}),
	validated: (params: DesignerAll) =>
		queryOptions({
			queryKey: [...DESIGNER_BASE_KEY, 'validated', params],
			queryFn: async () =>
				(await products.designer.validated(params)).data,
		}),
	reported: (params: DesignerAll) =>
		queryOptions({
			queryKey: [...DESIGNER_BASE_KEY, 'reported', params],
			queryFn: async () =>
				(await products.designer.reported(params)).data,
		}),
	single: (params: DesignerSingle) =>
		queryOptions({
			queryKey: [...DESIGNER_BASE_KEY, 'single', params],
			queryFn: async () => (await products.designer.single(params)).data,
		}),
};
