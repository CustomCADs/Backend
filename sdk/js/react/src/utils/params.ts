export const objectToSearchParams = (
	obj: object,
	filter?: (key: string) => boolean,
) =>
	new URLSearchParams(
		Object.entries(obj)
			.filter(
				([key, value]) =>
					value !== undefined &&
					value !== null &&
					(filter?.(key) ?? true),
			)
			.map(([key, value]) => [key, String(value)]),
	).toString();
