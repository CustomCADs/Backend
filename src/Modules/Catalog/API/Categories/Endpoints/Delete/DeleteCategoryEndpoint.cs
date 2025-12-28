using CustomCADs.Modules.Catalog.Application.Categories.Commands.Internal.Delete;

namespace CustomCADs.Modules.Catalog.API.Categories.Endpoints.Delete;

public sealed class DeleteCategoryEndpoint(IRequestSender sender)
	: Endpoint<DeleteCategoryRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<CategoriesGroup>();
		Description(x => x
			.WithSummary("Delete")
			.WithDescription("Delete a Category")
		);
	}

	public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new DeleteCategoryCommand(
				Id: CategoryId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
