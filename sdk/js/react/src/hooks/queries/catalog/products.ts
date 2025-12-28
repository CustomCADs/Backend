import { queryOptions } from '@tanstack/react-query';
import { productsApi } from '@/api';
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
			queryKey: [...GALLERY_BASE_KEY, 'all', params] as const,
			queryFn: () => productsApi.gallery.all(params),
		}),
	single: (params: GallerySingle) =>
		queryOptions({
			queryKey: [...GALLERY_BASE_KEY, 'single', params] as const,
			queryFn: () => productsApi.gallery.single(params),
		}),
	sortings: queryOptions({
		queryKey: [...GALLERY_BASE_KEY, 'sortings'] as const,
		queryFn: productsApi.gallery.sortings,
	}),
};

const CREATOR_BASE_KEY = ['products', 'creator'] as const;
export const creator = {
	all: (params: CreatorAll) =>
		queryOptions({
			queryKey: [...CREATOR_BASE_KEY, 'all', params] as const,
			queryFn: () => productsApi.creator.all(params),
		}),
	single: (params: CreatorSingle) =>
		queryOptions({
			queryKey: [...CREATOR_BASE_KEY, 'single', params] as const,
			queryFn: () => productsApi.creator.single(params),
		}),
	recent: (params: CreatorRecent) =>
		queryOptions({
			queryKey: [...CREATOR_BASE_KEY, 'recent'] as const,
			queryFn: () => productsApi.creator.recent(params),
		}),
	stats: queryOptions({
		queryKey: [...CREATOR_BASE_KEY, 'stats'] as const,
		queryFn: productsApi.creator.stats,
	}),
};

const DESIGNER_BASE_KEY = ['products', 'designer'] as const;
export const designer = {
	unchecked: (params: DesignerAll) =>
		queryOptions({
			queryKey: [...DESIGNER_BASE_KEY, 'unchecked', params] as const,
			queryFn: () => productsApi.designer.unchecked(params),
		}),
	validated: (params: DesignerAll) =>
		queryOptions({
			queryKey: [...DESIGNER_BASE_KEY, 'validated', params] as const,
			queryFn: () => productsApi.designer.validated(params),
		}),
	reported: (params: DesignerAll) =>
		queryOptions({
			queryKey: [...DESIGNER_BASE_KEY, 'reported', params] as const,
			queryFn: () => productsApi.designer.reported(params),
		}),
	single: (params: DesignerSingle) =>
		queryOptions({
			queryKey: [...DESIGNER_BASE_KEY, 'single', params] as const,
			queryFn: () => productsApi.designer.single(params),
		}),
};
