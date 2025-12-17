using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Tags;

namespace CustomCADs.Modules.Catalog.Application.Tags.Commands.Internal.Edit;

public sealed class EditTagHandler(
	ITagReads reads,
	IUnitOfWork uow,
	BaseCachingService<TagId, Tag> cache
) : ICommandHandler<EditTagCommand>
{
	public async Task Handle(EditTagCommand req, CancellationToken ct)
	{
		Tag tag = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Tag>.ById(req.Id);

		tag.SetName(req.Name);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(tag.Id, tag).ConfigureAwait(false);
	}
}
