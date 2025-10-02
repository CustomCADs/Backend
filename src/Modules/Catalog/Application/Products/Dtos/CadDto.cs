namespace CustomCADs.Catalog.Application.Products.Dtos;

public sealed record CadDto(
	string Key,
	string ContentType,
	decimal Volume
);
