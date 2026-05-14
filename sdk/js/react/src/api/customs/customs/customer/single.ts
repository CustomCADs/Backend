import {
	AcceptedCustomDto,
	CompletedCustomDto,
	FinishedCustomDto,
	CUSTOMS_CUSTOMER_BASE_PATH,
	CustomCategory,
} from '@/api/customs/common';

export type Request = {
	id: string;
};

export type Response = {
	id: string;
	name: string;
	description: string;
	orderedAt: string;
	status: string;
	forDelivery: boolean;
	category: CustomCategory | null;
	accepted: AcceptedCustomDto | null;
	finished: FinishedCustomDto | null;
	completed: CompletedCustomDto | null;
};

export const url = (req: Request) => `${CUSTOMS_CUSTOMER_BASE_PATH}/${req.id}`;
