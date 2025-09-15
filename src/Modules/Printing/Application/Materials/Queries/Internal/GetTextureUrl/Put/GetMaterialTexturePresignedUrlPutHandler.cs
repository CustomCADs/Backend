using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Put;

public sealed class GetMaterialTexturePresignedUrlPutHandler(
	IMaterialReads reads,
	BaseCachingService<MaterialId, Material> cache,
	IRequestSender sender
) : IQueryHandler<GetMaterialTexturePresignedUrlPutQuery, string>
{
	public async Task<string> Handle(GetMaterialTexturePresignedUrlPutQuery req, CancellationToken ct)
	{
		Material material = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Material>.ById(req.Id)
		).ConfigureAwait(false);

		return await sender.SendQueryAsync(
			query: new GetImagePresignedUrlPutByIdQuery(
				Id: material.TextureId,
				NewFile: req.NewImage
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
