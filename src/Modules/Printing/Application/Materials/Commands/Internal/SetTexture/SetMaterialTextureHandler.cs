using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Images.Commands;

namespace CustomCADs.Printing.Application.Materials.Commands.Internal.SetTexture;

public sealed class SetMaterialTextureHandler(IMaterialReads reads, BaseCachingService<MaterialId, Material> cache, IRequestSender sender)
	: ICommandHandler<SetMaterialTextureCommand>
{
	public async Task Handle(SetMaterialTextureCommand req, CancellationToken ct)
	{
		Material material = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Material>.ById(req.Id)
		).ConfigureAwait(false);

		await sender.SendCommandAsync(
			command: new EditImageCommand(
				Id: material.TextureId,
				ContentType: req.ContentType
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
