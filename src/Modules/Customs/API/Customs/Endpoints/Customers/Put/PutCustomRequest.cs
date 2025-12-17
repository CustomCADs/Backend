namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Put;

public sealed record PutCustomRequest(
	Guid Id,
	string Name,
	string Description,
	int? CategoryId
);
