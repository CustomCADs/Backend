using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Delete;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Delete;

public sealed class DeleteCustomEndpoint(IRequestSender sender)
	: Endpoint<DeleteCustomRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Delete")
			.WithDescription("Delete your Custom")
		);
	}

	public override async Task HandleAsync(DeleteCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteCustomCommand(
				Id: CustomId.New(req.Id),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
