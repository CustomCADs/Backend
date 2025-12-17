using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.SetCategory;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer.Patch.PatchCategory;

public class PatchCustomCategoryEndpoint(IRequestSender sender) : Endpoint<PatchCustomCategoryRequest>
{
	public override void Configure()
	{
		Patch("category");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Category")
			.WithDescription("Modify a Custom's Category")
		);
	}

	public override async Task HandleAsync(PatchCustomCategoryRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DesignerSetCustomCategoryCommand(
				Id: CustomId.New(req.Id),
				CategoryId: CategoryId.New(req.CategoryId),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
