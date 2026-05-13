export const EXCHANGE_RATES = {
	EUR: { rate: 1, symbol: '€', language: '', display: true },
	USD: { rate: 1.1646, symbol: '$', language: 'en-US', display: true },
	JPY: { rate: 173.1, symbol: '¥', language: 'ja-JP', display: true },
	BGN: { rate: 1.9558, symbol: 'лв', language: 'bg-BG', display: false },
	CZK: { rate: 24.485, symbol: 'Kč', language: 'cs-CZ', display: true },
	DKK: { rate: 7.4632, symbol: 'kr', language: 'da-DK', display: true },
	GBP: { rate: 0.8702, symbol: '£', language: 'en-GB', display: true },
	HUF: { rate: 395.58, symbol: 'Ft', language: 'hu-HU', display: true },
	PLN: { rate: 4.2653, symbol: 'zł', language: 'pl-PL', display: true },
	RON: { rate: 5.0822, symbol: 'lei', language: 'ro-RO', display: true },
	SEK: { rate: 11.003, symbol: 'kr', language: 'sv-SE', display: true },
	CHF: { rate: 0.9366, symbol: 'CHF', language: 'fr-CH', display: true },
	ISK: { rate: 143.6, symbol: 'Íkr', language: 'is-IS', display: true },
	NOK: { rate: 11.674, symbol: 'kr', language: 'no-NO', display: true },
	TRY: { rate: 47.9289, symbol: '₺', language: 'tr-TR', display: true },
	AUD: { rate: 1.7897, symbol: 'A$', language: 'en-AU', display: true },
	BRL: { rate: 6.3757, symbol: 'R$', language: 'pt-BR', display: true },
	CAD: { rate: 1.6056, symbol: 'CA$', language: 'en-CA', display: true },
	CNY: { rate: 8.3202, symbol: '¥', language: 'zh-CN', display: true },
	HKD: { rate: 9.0915, symbol: 'HK$', language: 'zh-HK', display: true },
	IDR: { rate: 19110.27, symbol: 'Rp', language: 'id-ID', display: true },
	ILS: { rate: 3.9478, symbol: '₪', language: 'he-IL', display: true },
	INR: { rate: 102.5795, symbol: '₹', language: 'hi-IN', display: true },
	KRW: { rate: 1625.01, symbol: '₩', language: 'ko-KR', display: true },
	MXN: { rate: 21.8726, symbol: '$', language: 'es-MX', display: true },
	MYR: { rate: 4.9263, symbol: 'RM', language: 'ms-MY', display: true },
	NZD: { rate: 1.9892, symbol: '$', language: 'en-NZ', display: true },
	PHP: { rate: 66.762, symbol: '₱', language: 'fil-PH', display: true },
	SGD: { rate: 1.5006, symbol: 'S$', language: 'en-SG', display: true },
	THB: { rate: 37.704, symbol: '฿', language: 'th-TH', display: true },
	ZAR: { rate: 20.6439, symbol: 'R', language: 'en-ZA', display: true },
} as const;

export type Currency = keyof typeof EXCHANGE_RATES;

export const CURRENCIES = Object.entries(EXCHANGE_RATES)
	.filter(([, v]) => v.display)
	.map(([k]) => k) as Array<Currency>;
