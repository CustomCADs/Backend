export * from './types';
export { axios, setBaseUrl } from './axios';

export * as exchangeRatesApi from './common/exchange-rates';

export * as accountsApi from './accounts/accounts';
export * as rolesApi from './accounts/roles';
export * as activeCartsApi from './carts/active';
export * as purchasedCartsApi from './carts/purchased';
export * as categoriesApi from './catalog/categories';
export * as tagsApi from './catalog/tags';

import * as productsAdmin from './catalog/products/admin';
import * as productsCreator from './catalog/products/creator';
import * as productsDesigner from './catalog/products/designer';
import * as productsGallery from './catalog/products/gallery';
export const productsApi = {
	admin: productsAdmin,
	creator: productsCreator,
	designer: productsDesigner,
	gallery: productsGallery,
};

import * as customsAdmin from './customs/customs/admin';
import * as customsCustomer from './customs/customs/customer';
import * as customsDesigner from './customs/customs/designer';
export const customsApi = {
	admin: customsAdmin,
	customer: customsCustomer,
	designer: customsDesigner,
};

export * as shipmentsApi from './delivery/shipments';
export * as imagesApi from './files/images';
export * as cadsApi from './files/cads';
export * as identityApi from './identity/identity';
export * as notificationsApi from './notifications/notifications';
export * as materialsApi from './printing/materials';
export * as customizationsApi from './printing/customizations';
