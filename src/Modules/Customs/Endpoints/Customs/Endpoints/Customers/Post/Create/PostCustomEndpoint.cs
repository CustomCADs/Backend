using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Single;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Create;

public sealed class PostCustomEndpoint(IRequestSender sender)
	: Endpoint<PostCustomRequest, PostCustomResponse>
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
				CallerId: User.GetAccountId(),
				CategoryId: CategoryId.New(req.CategoryId)
			),
			ct: ct
		).ConfigureAwait(false);

		CustomerGetCustomByIdDto custom = await sender.SendQueryAsync(
			query: new CustomerGetCustomByIdQuery(
				Id: id,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		PostCustomResponse response = custom.ToPostResponse();
		await Send.CreatedAtAsync<GetCustomEndpoint>(
			routeValues: new { Id = id.Value },
			responseBody: response
		).ConfigureAwait(false);
	}
}
