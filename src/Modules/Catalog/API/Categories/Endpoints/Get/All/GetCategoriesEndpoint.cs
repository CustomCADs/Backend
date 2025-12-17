using CustomCADs.Modules.Catalog.Application.Categories.Queries.Internal.GetAll;

namespace CustomCADs.Modules.Catalog.API.Categories.Endpoints.Get.All;

public sealed class GetCategoriesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<CategoryResponse[]>
{
	public override void Configure()
	{
		Get("");
		AllowAnonymous();
		Group<CategoriesGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all Categories")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		IEnumerable<CategoryReadDto> categories = await sender.SendQueryAsync(
			query: new GetAllCategoriesQuery(),
			ct: ct
		).ConfigureAwait(false);

		CategoryResponse[] response = [.. categories.Select(c => c.ToResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
