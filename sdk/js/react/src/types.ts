import { EXCHANGE_RATES } from './constants';

export type Currency = keyof typeof EXCHANGE_RATES;

export type Rates = {
	[C in Currency]: (typeof EXCHANGE_RATES)[C]['rate'];
};
export type Rate = Rates[Currency];

export type Symbols = {
	[C in Currency]: (typeof EXCHANGE_RATES)[C]['symbol'];
};
export type Symbol = Symbols[Currency];
