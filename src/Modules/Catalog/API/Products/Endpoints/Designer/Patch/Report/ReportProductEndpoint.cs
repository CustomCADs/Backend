using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.SetStatus;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Designer.Patch.Report;

public sealed class ReportProductEndpoint(IRequestSender sender)
	: Endpoint<ReportProductRequest>
{
	public override void Configure()
	{
		Patch("report");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Report")
			.WithDescription("Set a Product's Status to Reported")
		);
	}

	public override async Task HandleAsync(ReportProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new SetProductStatusCommand(
				Id: ProductId.New(req.Id),
				Status: ProductStatus.Reported,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
