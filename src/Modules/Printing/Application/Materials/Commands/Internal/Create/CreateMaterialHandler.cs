using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Images.Commands;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Printing.Application.Materials.Commands.Internal.Create;

public sealed class CreateMaterialHandler(
	IWrites<Material> writes,
	IUnitOfWork uow,
	BaseCachingService<MaterialId, Material> cache,
	IRequestSender sender
) : ICommandHandler<CreateMaterialCommand, MaterialId>
{
	public async Task<MaterialId> Handle(CreateMaterialCommand req, CancellationToken ct)
	{
		ImageId textureId = await sender.SendCommandAsync(
			command: new CreateImageCommand(
				Key: req.TextureKey,
				ContentType: req.TextureContentType
			),
			ct: ct
		).ConfigureAwait(false);

		Material material = await writes.AddAsync(
			entity: Material.Create(
				name: req.Name,
				density: req.Density,
				cost: req.Cost,
				textureId: textureId
			),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(material.Id, material).ConfigureAwait(false);

		return material.Id;
	}
}
