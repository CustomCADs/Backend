using CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Creator.Edit;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Creator.Put;

public sealed class PutProductEndpoint(IRequestSender sender)
	: Endpoint<PutProductRequest>
{
	public override void Configure()
	{
		Put("");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Edit")
			.WithDescription("Edit your Product")
		);
	}

	public override async Task HandleAsync(PutProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(new EditProductCommand(
				Id: ProductId.New(req.Id),
				Name: req.Name,
				Description: req.Description,
				CategoryId: CategoryId.New(req.CategoryId),
				Price: req.Price,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
