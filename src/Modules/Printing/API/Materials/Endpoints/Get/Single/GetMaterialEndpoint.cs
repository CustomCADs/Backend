using CustomCADs.Modules.Printing.API.Materials.Dtos;
using CustomCADs.Modules.Printing.Application.Materials.Queries.Internal.GetById;

namespace CustomCADs.Modules.Printing.API.Materials.Endpoints.Get.Single;

public sealed class GetMaterialEndpoint(IRequestSender sender)
	: Endpoint<GetMaterialRequest, MaterialResponse>
{
	public override void Configure()
	{
		Get("{id}");
		AllowAnonymous();
		Group<MaterialsGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See a Material")
		);
	}

	public override async Task HandleAsync(GetMaterialRequest req, CancellationToken ct)
	{
		MaterialDto category = await sender.SendQueryAsync(
			query: new GetMaterialByIdQuery(
				Id: MaterialId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		MaterialResponse response = category.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
