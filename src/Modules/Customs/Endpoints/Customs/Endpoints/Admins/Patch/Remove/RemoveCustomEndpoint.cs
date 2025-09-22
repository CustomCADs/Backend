using CustomCADs.Customs.Application.Customs.Commands.Internal.Admin.Remove;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Admins.Patch.Remove;

public sealed class RemoveCustomEndpoint(IRequestSender sender)
	: Endpoint<RemoveCustomRequest>
{
	public override void Configure()
	{
		Patch("remove");
		Group<AdminGroup>();
		Description(x => x
			.WithSummary("Remove")
			.WithDescription("Set an Custom's Status to Removed")
		);
	}

	public override async Task HandleAsync(RemoveCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new RemoveCustomCommand(
				Id: CustomId.New(req.Id),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
