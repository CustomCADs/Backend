using CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Get.Single;
using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Post.Create;

public sealed class PostCustomEndpoint(IRequestSender sender)
	: Endpoint<PostCustomRequest, PostCustomResponse, PostCustomMapper>
{
	public override void Configure()
	{
		Post("");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Request a Custom")
		);
	}

	public override async Task HandleAsync(PostCustomRequest req, CancellationToken ct)
	{
		CustomId id = await sender.SendCommandAsync(
			command: new CreateCustomCommand(
				Name: req.Name,
				Description: req.Description,
				ForDelivery: req.ForDelivery,
				CallerId: User.AccountId,
				CategoryId: CategoryId.New(req.CategoryId)
			),
			ct: ct
		).ConfigureAwait(false);

		CustomerGetCustomByIdDto custom = await sender.SendQueryAsync(
			query: new CustomerGetCustomByIdQuery(
				Id: id,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.CreatedAtAsync<GetCustomEndpoint>(
			routeValues: new { Id = id.Value },
			responseBody: Map.FromEntity(custom)
		).ConfigureAwait(false);
	}
}
