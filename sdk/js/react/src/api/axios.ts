import * as axios from 'axios';

const BASE_URL = import.meta.env.VITE_API_URL;
const API_VERSION = import.meta.env.VITE_API_VERSION ?? 'v1';

const instance = axios.default.create({
	baseURL: `${BASE_URL}/api/${API_VERSION}`,
	withCredentials: true,
	headers: {
		'Content-Type': 'application/json',
	},
});

const setBaseUrl = (url: string) => (instance.defaults.baseURL = url);

type ConfigProps = {
	idempotencyKey?: string;
	data?: unknown;
};
const config = (props?: ConfigProps): axios.AxiosRequestConfig => ({
	data: props?.data,
	headers: { ['Idempotency-Key']: props?.idempotencyKey },
});

export { instance as axios, config, setBaseUrl };
