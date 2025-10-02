using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;
using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetFiles;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Put.Products;

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
		ProductId id = ProductId.New(req.Id);

		await sender.SendCommandAsync(new EditProductCommand(
				Id: id,
				Name: req.Name,
				Description: req.Description,
				CategoryId: CategoryId.New(req.CategoryId),
				Price: req.Price,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await sender.SendCommandAsync(
			command: new SetProductFilesCommand(
				Id: id,
				Cad: req.Cad,
				Image: req.Image,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
