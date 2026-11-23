namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Mutations.Post.Create;

public sealed record PostCustomRequest(
	string Name,
	string Description,
	bool ForDelivery,
	int? CategoryId
);
