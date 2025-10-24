export const objectToSearchParams = (obj: object) =>
	new URLSearchParams(
		Object.entries(obj)
			.filter(([, value]) => value !== undefined && value !== null)
			.map(([key, value]) => [key, String(value)]),
	).toString();

export const unwrapResponse =
	<TResponse>(response: Promise<{ data: TResponse }>) =>
	() =>
		response.then((x) => x.data);
