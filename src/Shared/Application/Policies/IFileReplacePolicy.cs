using CustomCADs.Shared.Domain.Bases.Id;

namespace CustomCADs.Shared.Application.Policies;

public interface IFileReplacePolicy<TTypedId> where TTypedId : IEntityId<Guid>
{
	FileContextType Type { get; }
	record FileContext(TTypedId FileId, AccountId OwnerId, AccountId CallerId);
	Task EnsureReplaceGrantedAsync(FileContext context);
}
