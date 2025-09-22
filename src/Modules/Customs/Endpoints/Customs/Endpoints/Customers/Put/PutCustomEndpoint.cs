using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Edit;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Put;

public sealed class PutCustomEndpoint(IRequestSender sender)
	: Endpoint<PutCustomRequest>
{
	public override void Configure()
	{
		Put("");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Edit")
			.WithDescription("Edit your Custom")
		);
	}

	public override async Task HandleAsync(PutCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new EditCustomCommand(
				Id: CustomId.New(req.Id),
				Name: req.Name,
				Description: req.Description,
				CategoryId: CategoryId.New(req.CategoryId),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
