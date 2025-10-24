export * as exchangeRates from './common/exchange-rates';

export * as accounts from './accounts/accounts';
export * as roles from './accounts/roles';
export * as activeCarts from './carts/active';
export * as purchasedCarts from './carts/purchased';
export * as categories from './catalog/categories';
export * as tags from './catalog/tags';

import * as productsAdmin from './catalog/products/admin';
import * as productsCreator from './catalog/products/creator';
import * as productsDesigner from './catalog/products/designer';
import * as productsGallery from './catalog/products/gallery';
export const products = {
	admin: productsAdmin,
	creator: productsCreator,
	designer: productsDesigner,
	gallery: productsGallery,
};

import * as customsAdmin from './customs/customs/admin';
import * as customsCustomer from './customs/customs/customer';
import * as customsDesigner from './customs/customs/designer';
export const customs = {
	admin: customsAdmin,
	customer: customsCustomer,
	designer: customsDesigner,
};

export * as shipments from './delivery/shipments';
export * as images from './files/images';
export * as cads from './files/cads';
export * as identity from './identity/identity';
export * as notifications from './notifications/notifications';
export * as materials from './printing/materials';
export * as customizations from './printing/customizations';
