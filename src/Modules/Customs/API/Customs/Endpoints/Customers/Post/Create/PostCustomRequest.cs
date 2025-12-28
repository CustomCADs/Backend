namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Post.Create;

public sealed record PostCustomRequest(
	string Name,
	string Description,
	bool ForDelivery,
	int? CategoryId
);
