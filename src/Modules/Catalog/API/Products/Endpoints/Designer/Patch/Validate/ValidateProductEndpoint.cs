using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.SetStatus;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Patch.Validate;

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
			command: new SetProductStatusCommand(
				Id: ProductId.New(req.Id),
				Status: ProductStatus.Validated,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
