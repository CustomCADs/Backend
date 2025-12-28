using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Modules.Printing.Application.Materials.Commands.Internal.Create;

public sealed class CreateMaterialHandler(
	IWrites<Material> writes,
	IUnitOfWork uow,
	BaseCachingService<MaterialId, Material> cache,
	IRequestSender sender
) : ICommandHandler<CreateMaterialCommand, MaterialId>
{
	public async Task<MaterialId> Handle(CreateMaterialCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new ImageExistsByIdQuery(req.TextureId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Material>.ById(req.TextureId, "Texture");
		}

		Material material = await writes.AddAsync(
			entity: Material.Create(
				name: req.Name,
				density: req.Density,
				cost: req.Cost,
				textureId: req.TextureId
			),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(material.Id, material).ConfigureAwait(false);

		return material.Id;
	}
}
