using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Modules.Catalog.Domain.Tags;

namespace CustomCADs.Modules.Catalog.Application.Tags.Commands.Internal.Delete;

public sealed class DeleteTagHandler(
	ITagReads reads,
	ITagWrites writes,
	IUnitOfWork uow,
	BaseCachingService<TagId, Tag> cache
) : ICommandHandler<DeleteTagCommand>
{
	public async Task Handle(DeleteTagCommand req, CancellationToken ct)
	{
		Tag tag = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Tag>.ById(req.Id);

		writes.Remove(tag);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.ClearAsync(tag.Id).ConfigureAwait(false);
	}
}
