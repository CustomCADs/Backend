using CustomCADs.Modules.Catalog.Application.Categories.Commands.Internal.Edit;

namespace CustomCADs.Modules.Catalog.API.Categories.Endpoints.Put;

public sealed class PutCategoryEndpoint(IRequestSender sender)
	: Endpoint<PutCategoryRequest>
{
	public override void Configure()
	{
		Put("");
		Group<CategoriesGroup>();
		Description(x => x
			.WithSummary("Edit")
			.WithDescription("Edit a Category")
		);
	}

	public override async Task HandleAsync(PutCategoryRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new EditCategoryCommand(
				Id: CategoryId.New(req.Id),
				Dto: new CategoryWriteDto(req.Name, req.Description)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
