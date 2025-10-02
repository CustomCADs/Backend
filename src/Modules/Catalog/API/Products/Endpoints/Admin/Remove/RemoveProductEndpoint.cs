using CustomCADs.Catalog.Application.Products.Commands.Internal.Admin.Remove;

namespace CustomCADs.Catalog.API.Products.Endpoints.Admin.Remove;

public sealed class RemoveProductEndpoint(IRequestSender sender)
	: Endpoint<RemoveProductRequest>
{
	public override void Configure()
	{
		Patch("report");
		Group<AdminGroup>();
		Description(x => x
			.WithSummary("Remove")
			.WithDescription("Set a Product's Status to Removed")
		);
	}

	public override async Task HandleAsync(RemoveProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new RemoveProductCommand(
				Id: ProductId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
