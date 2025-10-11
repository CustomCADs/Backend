using CustomCADs.Printing.API.Materials.Dtos;
using CustomCADs.Printing.API.Materials.Endpoints.Get.Single;
using CustomCADs.Printing.Application.Materials.Commands.Internal.Create;
using CustomCADs.Printing.Application.Materials.Queries.Internal.GetById;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Printing.API.Materials.Endpoints.Post;

public sealed class PostMaterialEndpoint(IRequestSender sender)
	: Endpoint<PostMaterialRequest, MaterialResponse>
{
	public override void Configure()
	{
		Post("");
		Group<MaterialsGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Add a Material")
		);
	}

	public override async Task HandleAsync(PostMaterialRequest req, CancellationToken ct)
	{
		MaterialId id = await sender.SendCommandAsync(
			command: new CreateMaterialCommand(
				Name: req.Name,
				Density: req.Density,
				Cost: req.Cost,
				TextureId: ImageId.New(req.TextureId)
			),
			ct: ct
		).ConfigureAwait(false);

		MaterialDto material = await sender.SendQueryAsync(
			query: new GetMaterialByIdQuery(id),
			ct: ct
		).ConfigureAwait(false);

		MaterialResponse response = material.ToResponse();
		await Send.CreatedAtAsync<GetMaterialEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
	}
}
