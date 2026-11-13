export const ROLES = [
	'Customer',
	'Contributor',
	'Designer',
	'Administrator',
] as const;
export type Role = (typeof ROLES)[number];
