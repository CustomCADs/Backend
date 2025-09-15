using CustomCADs.Catalog.Application.Categories.Queries.Internal.GetById;

namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints.Get.Single;

public sealed class GetCategoryEndpoint(IRequestSender sender)
	: Endpoint<GetCategoryRequest, CategoryResponse>
{
	public override void Configure()
	{
		Get("{id}");
		AllowAnonymous();
		Group<CategoriesGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See a Category")
		);
	}

	public override async Task HandleAsync(GetCategoryRequest req, CancellationToken ct)
	{
		CategoryReadDto category = await sender.SendQueryAsync(
			query: new GetCategoryByIdQuery(
				Id: CategoryId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		CategoryResponse response = category.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
