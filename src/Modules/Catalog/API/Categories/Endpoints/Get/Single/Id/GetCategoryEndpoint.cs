using CustomCADs.Modules.Catalog.Application.Categories.Queries.Internal.GetById;

namespace CustomCADs.Modules.Catalog.API.Categories.Endpoints.Get.Single.Id;

public sealed class GetCategoryEndpoint(IRequestSender sender)
	: Endpoint<GetCategoryRequest, CategoryResponse>
{
	public override void Configure()
	{
		Get("{id:int}");
		AllowAnonymous();
		Group<CategoriesGroup>();
		Description(x => x
			.WithSummary("Single (Id)")
			.WithDescription("See a Category by its Id")
		);
	}

	public override async Task HandleAsync(GetCategoryRequest req, CancellationToken ct)
	{
		CategoryReadDto category = await sender.SendQueryAsync(
				query: new GetCategoryByIdQuery(CategoryId.New(req.Id)),
				ct: ct
			).ConfigureAwait(false); ;
		CategoryResponse response = category.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
