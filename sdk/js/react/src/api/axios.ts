import * as axios from 'axios';

const instance = axios.default.create({
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
