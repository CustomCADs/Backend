using CustomCADs.Catalog.Application.Categories.Commands.Internal.Create;
using CustomCADs.Catalog.Endpoints.Categories.Endpoints.Get.Single;

namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints.Post;

public sealed class PostCategoryEndpoint(IRequestSender sender)
	: Endpoint<PostCategoryRequest, CategoryResponse>
{
	public override void Configure()
	{
		Post("");
		Group<CategoriesGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Add a Category")
		);
	}

	public override async Task HandleAsync(PostCategoryRequest req, CancellationToken ct)
	{
		CategoryId id = await sender.SendCommandAsync(
			command: new CreateCategoryCommand(
				Dto: new CategoryWriteDto(req.Name, req.Description)
			),
			ct: ct
		).ConfigureAwait(false);

		CategoryResponse response = new(id.Value, req.Name, req.Description);
		await Send.CreatedAtAsync<GetCategoryEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
	}
}
