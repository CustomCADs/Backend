using CustomCADs.Shared.Domain.Bases.Id;

namespace CustomCADs.Shared.Application.Policies;

public interface IFileUploadPolicy<TTypedId> where TTypedId : IEntityId<Guid>
{
	public FileContextType Type { get; }
	record FileContext(AccountId CallerId);
	Task EnsureUploadGrantedAsync(FileContext context);
}
