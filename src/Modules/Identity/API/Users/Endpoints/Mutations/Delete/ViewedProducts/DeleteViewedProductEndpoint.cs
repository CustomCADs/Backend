using CustomCADs.Shared.Application.UseCases.Accounts.Commands;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Mutations.Delete.ViewedProducts;

public sealed class DeleteViewedProductEndpoint(IRequestSender sender)
	: Endpoint<DeleteViewedProductRequest>
{
	public override void Configure()
	{
		Delete("viewed-product");
		Group<IdentityGroup>();
		Description(x => x
			.WithSummary("Remove Viewed Product")
			.WithDescription("Remove a Viewed Product")
		);
	}

	public override async Task HandleAsync(DeleteViewedProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteViewedProductCommand(
				ProductId: ProductId.New(req.ProductId),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
