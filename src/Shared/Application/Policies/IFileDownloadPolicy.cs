using CustomCADs.Shared.Domain.Bases.Id;

namespace CustomCADs.Shared.Application.Policies;

public interface IFileDownloadPolicy<TTypedId> where TTypedId : IEntityId<Guid>
{
	public FileContextType Type { get; }
	record FileContext(TTypedId FileId, AccountId OwnerId, AccountId CallerId);
	Task EnsureDownloadGrantedAsync(FileContext context);
}
