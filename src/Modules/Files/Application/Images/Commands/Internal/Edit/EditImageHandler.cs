using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;

namespace CustomCADs.Files.Application.Images.Commands.Internal.Edit;

public sealed class EditImageHandler(
	IImageReads reads,
	IUnitOfWork uow,
	BaseCachingService<ImageId, Image> cache
) : ICommandHandler<EditImageCommand>
{
	public async Task Handle(EditImageCommand req, CancellationToken ct = default)
	{
		Image image = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Image>.ById(req.Id);

		image.SetContentType(req.ContentType);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(image.Id, image).ConfigureAwait(false);
	}
}
