import { CATEGORY_BASE_PATH } from '../common';

export type Request =
	| {
			type: 'by-id';
			id: number;
	  }
	| {
			type: 'by-name';
			name: string;
	  };

export const url = (req: Request) =>
	`${CATEGORY_BASE_PATH}/${req.type === 'by-id' ? req.id : req.name}`;
