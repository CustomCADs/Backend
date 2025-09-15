using CustomCADs.Printing.Application.Materials.Queries.Internal.GetAll;
using CustomCADs.Printing.Endpoints.Materials.Dtos;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Get.All;

public sealed class GetCategoriesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<MaterialResponse[]>
{
	public override void Configure()
	{
		Get("");
		AllowAnonymous();
		Group<MaterialsGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all Materials")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		IEnumerable<MaterialDto> categories = await sender.SendQueryAsync(
			query: new GetAllMaterialsQuery(),
			ct: ct
		).ConfigureAwait(false);

		MaterialResponse[] response = [.. categories.Select(x => x.ToResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
