using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.Report;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer.Patch.Report;

public sealed class ReportCustomEndpoint(IRequestSender sender)
	: Endpoint<ReportCustomRequest>
{
	public override void Configure()
	{
		Patch("report");
		Group<DesignerGroup>();
		Description(x => x
			.WithSummary("Report")
			.WithDescription("Set a Custom's Status to Reported")
		);
	}

	public override async Task HandleAsync(ReportCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new ReportCustomCommand(
				Id: CustomId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
