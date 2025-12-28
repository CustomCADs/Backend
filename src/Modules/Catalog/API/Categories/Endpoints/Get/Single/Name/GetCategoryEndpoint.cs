using CustomCADs.Modules.Catalog.Application.Categories.Queries.Internal.GetByName;

namespace CustomCADs.Modules.Catalog.API.Categories.Endpoints.Get.Single.Name;

public sealed class GetCategoryEndpoint(IRequestSender sender)
	: Endpoint<GetCategoryRequest, CategoryResponse>
{
	public override void Configure()
	{
		Get("{name}");
		AllowAnonymous();
		Group<CategoriesGroup>();
		Description(x => x
			.WithSummary("Single (Name)")
			.WithDescription("See a Category by its Name")
		);
	}

	public override async Task HandleAsync(GetCategoryRequest req, CancellationToken ct)
	{
		CategoryReadDto category = await sender.SendQueryAsync(
				query: new GetCategoryByNameQuery(req.Name),
				ct: ct
			).ConfigureAwait(false); ;
		CategoryResponse response = category.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
