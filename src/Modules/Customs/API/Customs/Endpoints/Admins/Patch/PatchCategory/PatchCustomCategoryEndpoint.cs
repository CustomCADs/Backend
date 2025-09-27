using CustomCADs.Customs.Application.Customs.Commands.Internal.Admin.SetCategory;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Customs.API.Customs.Endpoints.Admins.Patch.PatchCategory;

public class PatchCustomCategoryEndpoint(IRequestSender sender) : Endpoint<PatchCustomCategoryRequest>
{
	public override void Configure()
	{
		Patch("category");
		Group<AdminGroup>();
		Description(d => d
			.WithSummary("Category")
			.WithDescription("Modify a Custom's Category")
		);
	}

	public override async Task HandleAsync(PatchCustomCategoryRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new AdminSetCustomCategoryCommand(
				Id: CustomId.New(req.Id),
				CategoryId: CategoryId.New(req.CategoryId)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
