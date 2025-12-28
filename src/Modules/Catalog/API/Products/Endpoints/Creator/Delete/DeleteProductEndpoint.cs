using CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Creator.Delete;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Delete;

public sealed class DeleteProductEndpoint(IRequestSender sender)
	: Endpoint<DeleteProductRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Delete")
			.WithDescription("Delete your Product")
		);
	}

	public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteProductCommand(
				Id: ProductId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
