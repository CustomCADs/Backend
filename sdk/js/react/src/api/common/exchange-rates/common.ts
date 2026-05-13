import { Currency } from '@/constants';

export type ExchangeRate = {
	date: string;
	currency: Currency;
	rate: number;
};
