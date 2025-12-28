using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Modules.Catalog.Domain.Tags;

namespace CustomCADs.Modules.Catalog.Application.Tags.Commands.Internal.Create;

public sealed class CreateTagHandler(
	ITagWrites writes,
	IUnitOfWork uow,
	BaseCachingService<TagId, Tag> cache
) : ICommandHandler<CreateTagCommand, TagId>
{
	public async Task<TagId> Handle(CreateTagCommand req, CancellationToken ct)
	{
		Tag tag = await writes.AddAsync(
			tag: Tag.Create(req.Name),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(tag.Id, tag).ConfigureAwait(false);
		return tag.Id;
	}
}
