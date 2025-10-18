using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;

namespace CustomCADs.Files.Application.Images.Commands.Internal.Create;

public sealed class CreateImageHandler(
	IWrites<Image> writes,
	IUnitOfWork uow,
	BaseCachingService<ImageId, Image> cache
) : ICommandHandler<CreateImageCommand, ImageId>
{
	public async Task<ImageId> Handle(CreateImageCommand req, CancellationToken ct)
	{
		Image image = await writes.AddAsync(
			entity: Image.Create(
				key: req.GeneratedKey,
				contentType: req.ContentType,
				ownerId: req.CallerId
			),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(image.Id, image).ConfigureAwait(false);

		return image.Id;
	}
}
