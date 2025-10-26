import { Currency } from '@/types';

export type ExchangeRate = {
	date: string;
	currency: Currency;
	rate: number;
};
