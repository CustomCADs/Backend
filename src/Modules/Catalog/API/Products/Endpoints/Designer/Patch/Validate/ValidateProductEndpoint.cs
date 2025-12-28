using CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.Validate;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Designer.Patch.Validate;

public sealed class ValidateProductEndpoint(IRequestSender sender)
	: Endpoint<ValidateProductRequest>
{
	public override void Configure()
	{
		Patch("validate");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Validate")
			.WithDescription("Set a Product's Status to Validated")
		);
	}

	public override async Task HandleAsync(ValidateProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new ValidateProductCommand(
				Id: ProductId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
