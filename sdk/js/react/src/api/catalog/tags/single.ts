import { TAG_BASE_PATH } from '../common';

export type Request =
	| { type: 'by-id'; id: string }
	| { type: 'by-name'; name: string };

export const url = (req: Request) =>
	`${TAG_BASE_PATH}/${req.type === 'by-id' ? req.id : req.name}`;
