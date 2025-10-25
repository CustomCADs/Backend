import { mutationOptions } from '@tanstack/react-query';
import { activeCarts as api } from '@/api';
import { Request as AddItem } from '@/api/carts/active/add-item';
import { Request as ChangeItemQuantity } from '@/api/carts/active/change-quantity';
import { Request as ToggleItemDelivery } from '@/api/carts/active/toggle-for-delivery';
import { Request as RemoveItem } from '@/api/carts/active/remove-item';
import { Request as Purchase } from '@/api/carts/active/purchase';
import { Request as PurchaseWithDelivery } from '@/api/carts/active/purchase-delivery';

const BASE_KEY = ['active-carts'] as const;
export const activeCarts = {
	addItem: mutationOptions({
		mutationKey: [...BASE_KEY, 'add-item'],
		mutationFn: async (params: AddItem) => (await api.addItem(params)).data,
	}),
	toggleItemForDelivery: mutationOptions({
		mutationKey: [...BASE_KEY, 'toggle-item-for-delivery'],
		mutationFn: async (req: ToggleItemDelivery) =>
			(await api.toggleItemForDelivery(req)).data,
	}),
	increaseItemQuantity: mutationOptions({
		mutationKey: [...BASE_KEY, 'increase-item-quantity'],
		mutationFn: async (params: ChangeItemQuantity) =>
			(await api.increaseItemQuantity(params)).data,
	}),
	decreaseItemQuantity: mutationOptions({
		mutationKey: [...BASE_KEY, 'decrease-item-quantity'],
		mutationFn: async (params: ChangeItemQuantity) =>
			(await api.decreaseItemQuantity(params)).data,
	}),
	removeItem: mutationOptions({
		mutationKey: [...BASE_KEY, 'remove-item'],
		mutationFn: async (params: RemoveItem) =>
			(await api.removeItem(params)).data,
	}),
	purchase: mutationOptions({
		mutationKey: [...BASE_KEY, 'purchase'],
		mutationFn: async (req: Purchase) => (await api.purchase(req)).data,
	}),
	purchaseWithDelivery: mutationOptions({
		mutationKey: [...BASE_KEY, 'purchase-with-delivery'],
		mutationFn: async (req: PurchaseWithDelivery) =>
			(await api.purchaseWithDelivery(req)).data,
	}),
};
